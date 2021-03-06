﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NClass.Core;
using NClass.CSharp;

using System.Text.RegularExpressions;
using System.Linq;

using System.IO;
using System.Text;

using DatabaseSchemaReader;
using DatabaseSchemaReader.DataSchema;

using DatabaseSchemaReader.SqlGen;

namespace NClass.CodeGenerator
{
    internal sealed class CSharpSQLSourceFileGenerator 
        : SourceFileGenerator
    {
        List<string> entities;
        List<string> properties;

        bool useLowercaseUnderscored;

        DatabaseSchema schema;

        public CSharpSQLSourceFileGenerator
            (string rootNamespace, Model model)
            : base(rootNamespace, model)
        {}

        protected override string Extension
        {
            get { return ".sql"; }
        }

        protected override void WriteFileContent()
        {
            entities = new List<string>();
            foreach (IEntity entity in Model.Entities)
            {
                entities.Add(entity.Name);
            }

            useLowercaseUnderscored = Settings.Default.UseLowercaseAndUnderscoredWordsInDb;
            var sqlToServerType = Settings.Default.SQLToServerType;

            schema = new DatabaseSchema(null, sqlToServerType);

            foreach (IEntity entity in Model.Entities)
            {
                if(entity is ClassType)
                {
                    var _class = entity as ClassType;
                    if(_class.Operations.Where(x => x is Property).Count() > 0)
                        CreateTable(_class);
                }
            }

            var ddlTables = new DdlGeneratorFactory(sqlToServerType).AllTablesGenerator(schema);
            ddlTables.IncludeSchema = false;

            Write(ddlTables.Write());
        }

        private DatabaseTable CreateTable(ClassType _class)
        {
            var name = PrefixedText(
                useLowercaseUnderscored
                ? LowercaseAndUnderscoredWord(_class.Name)
                : _class.Name
                );

            DatabaseTable table = schema.FindTableByName(name);

            if (table != null)
                return table;

            properties = new List<string>();
            foreach (Operation operation in _class.Operations)
            {
                if (operation is Property)
                    properties.Add(operation.Name);
            }

            List<Property> compositeKey = new List<Property>();

            int index = 0;

            if (entities.Contains(_class.Operations.ToList()[0].Type))
            {
                for (; index <= (_class.Operations.Count() - 1); index++)
                {
                    if (_class.Operations.ToList()[index] is Property)
                    {
                        Property property = (Property)_class.Operations.ToList()[index];

                        if (entities.Contains(property.Type))
                        {
                            compositeKey.Add(property);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            table = schema.AddTable(name);

            if (compositeKey.Count > 1)
            {
                foreach(var key in compositeKey)
                {
                    table.AddColumn(key.Name, GetTypeFromString(key.Type))
                        .AddForeignKey(CreateTable(GetClassByName(key.Type)).Name)
                        .AddLength(255)
                        .AddPrimaryKey();
                }
            }
            else
            {
                index = 0;
            }

            for (; index <= (_class.Operations.Count() - 1); index++)
            {
                if (_class.Operations.ToList()[index] is Property)
                {
                    Property property = (Property)_class.Operations.ToList()[index];
                    
                    try
                    {
                        CreateColumn(ref table, property);
                    }
                    catch (ArgumentException e)
                    {
                        System.Windows.Forms.MessageBox.Show(
                            string.Format("{0}, {1} - {2}", _class.Name, property.Name, e.Message),
                            "Error",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Error);
                        throw;
                    }
                }
            }

            return table;
        }

        private void CreateColumn(ref DatabaseTable table, Property property)
        {
            if (property.Name == properties[0])
            {
                table.AddColumn(
                    useLowercaseUnderscored
                    ? LowercaseAndUnderscoredWord(property.Name)
                    : property.Name,
                    GetTypeFromString(property.Type))
                    .AddLength(255)
                    .AddPrimaryKey();
            }
            else if (entities.Contains(property.Type))
            {
                table.AddColumn(
                    useLowercaseUnderscored
                    ? LowercaseAndUnderscoredWord(property.Name)
                    : property.Name,
                    GetTypeFromString(property.Type))
                    .AddLength(255)
                    .AddForeignKey(CreateTable(GetClassByName(property.Type)).Name);
            }
            else
            {
                table.AddColumn(
                    useLowercaseUnderscored
                    ? LowercaseAndUnderscoredWord(property.Name)
                    : property.Name,
                    GetTypeFromString(property.Type))
                    .AddPrecisionScale(18, 5)
                    .AddLength(255);
            }
        }

        private ClassType GetClassByName(string className)
        {
            return Model.Entities.SingleOrDefault(x => x is ClassType && x.Name == className) as ClassType;
        }

        public override string Generate(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string fileName = "script" + Extension;
                string path = Path.Combine(directory, fileName);

                using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))
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

        private Type GetTypeFromString(string _type)
        {
            Type t = null;

            Dictionary<string, Type> types = new Dictionary<string, Type>
            {
                { "bool", typeof(bool) },
                { "byte", typeof(byte) },
                { "byte[]", typeof(byte[]) },
                { "char", typeof(char) },
                { "decimal", typeof(decimal) },
                { "double", typeof(double) },
                { "float", typeof(float) },
                { "int", typeof(int) },
                { "long", typeof(long) },
                { "sbyte", typeof(sbyte) },
                { "short", typeof(short) },
                { "uint", typeof(uint) },
                { "ulong", typeof(ulong) },
                { "ushort", typeof(ushort) },
                { "string", typeof(string) },
                { "object", typeof(object) }
            };

            if(entities.Contains(_type))
            {
                var _class = Model.Entities.SingleOrDefault(x => x is ClassType && x.Name == _type) as ClassType;
                var properties = _class.Operations.Where(x => x is Property).ToList();
                _type = properties[0].Type;
            }
            
            if (types.ContainsKey(_type))
                t = types[_type];
            else if ((new Regex("^System", RegexOptions.IgnoreCase).IsMatch(_type)))
                t = System.Type.GetType(_type);
            else
                t = System.Type.GetType("System." + _type);

            if (t == null) throw new ArgumentException(string.Format("Cannot map type \"{0}\" to a database type.", _type));

            return t;
        }
    }
}
