// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.IO;
using System.Text;
using NClass.Core;
using System.Text.RegularExpressions;

using System.Linq;

namespace NClass.CodeGenerator
{
	public abstract class SourceFileGenerator
	{
		// This builder object is static to increase performance
		static StringBuilder codeBuilder;
		const int DefaultBuilderCapacity = 10240; // 10 KB

		TypeBase type;
		string rootNamespace;
        Model model;
		int indentLevel = 0;

		/// <exception cref="ArgumentNullException">
		/// <paramref name="type"/> is null.
		/// </exception>
		protected SourceFileGenerator(TypeBase type, string rootNamespace)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			this.type = type;
			this.rootNamespace = rootNamespace;
		}

        protected SourceFileGenerator(TypeBase type, string rootNamespace, Model model)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
            this.rootNamespace = rootNamespace;
            this.model = model;
        }

        protected SourceFileGenerator(string rootNamespace, Model model)
        {
            this.rootNamespace = rootNamespace;
            this.model = model;
        }

		protected TypeBase Type
		{
			get { return type; }
		}

        public string ProjectName
        {
            get
            {
                string projectName = Model.Project.Name;
                string modelName = Model.Name;

                if (string.Equals(projectName, modelName, StringComparison.OrdinalIgnoreCase))
                    return projectName;
                else
                    return projectName + "." + modelName;
            }
        }

		protected string RootNamespace
		{
			get { return rootNamespace; }
		}

        protected Model Model
        {
            get { return model; }
        }

        protected StringBuilder CodeBuilder
        {
            get
            {
                return codeBuilder;
            }
            set
            {
                codeBuilder = value;
            }
        }

		protected int IndentLevel
		{
			get
			{
				return indentLevel;
			}
			set
			{
				if (value >= 0)
					indentLevel = value;
			}
		}

		protected abstract string Extension
		{
			get;
		}

		/// <exception cref="FileGenerationException">
		/// An error has occured while generating the source file.
		/// </exception>
		public virtual string Generate(string directory)
		{
			try
			{
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);

				string fileName = Type.Name + Extension;
				fileName = Regex.Replace(fileName, @"\<(?<type>.+)\>", @"[${type}]");
				string path = Path.Combine(directory, fileName);

				using (StreamWriter writer = new StreamWriter(path, false, Encoding.Unicode))
				{
					WriteFileContent(writer);
				}
				return fileName;
			}
			catch (Exception ex)
			{
				throw new FileGenerationException(directory, ex);
			}
		}

		/// <exception cref="IOException">
		/// An I/O error occurs.
		/// </exception>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="TextWriter"/> is closed.
		/// </exception>
		protected void WriteFileContent(TextWriter writer)
		{
			if (codeBuilder == null)
				codeBuilder = new StringBuilder(DefaultBuilderCapacity);
			else
				codeBuilder.Length = 0;

			WriteFileContent();
			writer.Write(codeBuilder.ToString());
		}

		protected abstract void WriteFileContent();

		internal static void FinishWork()
		{
			codeBuilder = null;
		}

		protected void AddBlankLine()
		{
			AddBlankLine(false);
		}

		protected void AddBlankLine(bool indentation)
		{
			if (indentation)
				AddIndent();
			codeBuilder.AppendLine();
		}

        protected void WriteHeader()
        {
            WriteLine(
                string.Format(
                "// This code was generated by {0}", 
                GetVersionString()
                ));
        }

        protected string GetVersionString()
        {
            Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            
            if (currentVersion.Minor == 0)
            {
                return string.Format("NClass {0}.0", currentVersion.Major);
            }
            else
            {
                return string.Format("NClass {0}.{1:00}",
                    currentVersion.Major, currentVersion.Minor);
            }
        }

        public void WriteXmlComments(object obj)
        {
            if (obj is CompositeType)
            {

                var _type = obj as CompositeType;
                WriteLine(string.Format("/// <summary>{0}</summary>", _type.Name));
                if (_type.Operations.Where(x => x is Method).Count() > 0)
                {
                    Write("/// <remarks>");
                    foreach (var operation in _type.Operations)
                    {
                        if (operation is Method)
                            Write(string.Format("{0}, ", operation.Name), false);
                    }
                    Write("</remarks>", false);
                    WriteLine("");
                }
            }
            else if(obj is EnumType)
            {
                var _enum = obj as EnumType;
                WriteLine(string.Format("/// <summary>{0}</summary>", _enum.Name));
            }
            else if (obj is EnumValue)
            {
                var enumValue = obj as EnumValue;
                WriteLine(string.Format("/// <summary>{0}</summary>", enumValue.Name));
            }
            else if (obj is DelegateType)
            {
                var _delegate = obj as DelegateType;
                WriteLine(string.Format("/// <summary>{0}</summary>", _delegate.Name));
                foreach (var arg in _delegate.Arguments)
                    WriteLine(string.Format("/// <param name=\"{0}\">{0}</param>", arg.Name));
            }
            else if(obj is Method)
            {
                var method = obj as Method;
                WriteLine(string.Format("/// <summary>{0}</summary>", method.Name));
                if(method.HasParameter)
                    WriteLine("/// <param name=\"s\"> Parameter description for s goes here.</param>");
                if(!(method is Constructor))
                    WriteLine("/// <returns></returns>");
            }
            else if(obj is Property)
            {
                var property = obj as Property;
                WriteLine(string.Format("/// <summary>{0}</summary>", property.Name));
                WriteLine(string.Format("/// <value>{0}</value>", property.Name));
            }
            else if(obj is Field)
            {
                var field = obj as Field;
                WriteLine(string.Format("/// <summary>{0}</summary>", field.Name));
            }
        }

        protected void Write(string text)
        {
            Write(text, true);
        }

        protected void Write(string text, bool indentation)
        {
            if (indentation)
                AddIndent();
            codeBuilder.Append(text);
        }

		protected void WriteLine(string text)
		{
			WriteLine(text, true);
		}

		protected void WriteLine(string text, bool indentation)
		{
			if (indentation)
				AddIndent();
			codeBuilder.AppendLine(text);
		}

		private void AddIndent()
		{
			string indentString;
			if (Settings.Default.UseTabsForIndents)
				indentString = new string('\t', IndentLevel);
			else
				indentString = new string(' ', IndentLevel * Settings.Default.IndentSize);

			codeBuilder.Append(indentString);
		}

        protected string PrefixedText(string text)
        {
            return new PrefixedTextFormatter(Settings.Default.PrefixTable).FormatText(text);
        }

        protected string LowercaseAndUnderscoredWord(string text)
        {
            return new LowercaseAndUnderscoredWordTextFormatter().FormatText(text);
        }
	}
}