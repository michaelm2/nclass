﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NClass.CodeGenerator {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public int IndentSize {
            get {
                return ((int)(this["IndentSize"]));
            }
            set {
                this["IndentSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseTabsForIndents {
            get {
                return ((bool)(this["UseTabsForIndents"]));
            }
            set {
                this["UseTabsForIndents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>System</string>
  <string>System.Collections.Generic</string>
  <string>System.Text</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CSharpImportList {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CSharpImportList"]));
            }
            set {
                this["CSharpImportList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3." +
            "org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <s" +
            "tring>java.io.*</string>\r\n  <string>java.util.*</string>\r\n</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection JavaImportList {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["JavaImportList"]));
            }
            set {
                this["JavaImportList"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string DestinationPath {
            get {
                return ((string)(this["DestinationPath"]));
            }
            set {
                this["DestinationPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseNotImplementedExceptions {
            get {
                return ((bool)(this["UseNotImplementedExceptions"]));
            }
            set {
                this["UseNotImplementedExceptions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseAutomaticProperties {
            get {
                return ((bool)(this["UseAutomaticProperties"]));
            }
            set {
                this["UseAutomaticProperties"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GenerateNHibernateMapping {
            get {
                return ((bool)(this["GenerateNHibernateMapping"]));
            }
            set {
                this["GenerateNHibernateMapping"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("NHibernateXml")]
        public global::NClass.CodeGenerator.MappingType MappingType {
            get {
                return ((global::NClass.CodeGenerator.MappingType)(this["MappingType"]));
            }
            set {
                this["MappingType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Increment")]
        public global::NClass.CodeGenerator.IdGeneratorType IdGeneratorType {
            get {
                return ((global::NClass.CodeGenerator.IdGeneratorType)(this["IdGeneratorType"]));
            }
            set {
                this["IdGeneratorType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseLazyLoading {
            get {
                return ((bool)(this["UseLazyLoading"]));
            }
            set {
                this["UseLazyLoading"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseLowercaseAndUnderscoredWordsInDb {
            get {
                return ((bool)(this["UseLowercaseAndUnderscoredWordsInDb"]));
            }
            set {
                this["UseLowercaseAndUnderscoredWordsInDb"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pfx_")]
        public string PrefixTable {
            get {
                return ((string)(this["PrefixTable"]));
            }
            set {
                this["PrefixTable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GenerateCodeFromTemplates {
            get {
                return ((bool)(this["GenerateCodeFromTemplates"]));
            }
            set {
                this["GenerateCodeFromTemplates"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("VisualStudio2010")]
        public global::NClass.CodeGenerator.SolutionType SolutionType {
            get {
                return ((global::NClass.CodeGenerator.SolutionType)(this["SolutionType"]));
            }
            set {
                this["SolutionType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GenerateSQLCode {
            get {
                return ((bool)(this["GenerateSQLCode"]));
            }
            set {
                this["GenerateSQLCode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("SqlServer")]
        public global::DatabaseSchemaReader.DataSchema.SqlType SQLToServerType {
            get {
                return ((global::DatabaseSchemaReader.DataSchema.SqlType)(this["SQLToServerType"]));
            }
            set {
                this["SQLToServerType"] = value;
            }
        }
    }
}
