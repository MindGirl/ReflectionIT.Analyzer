﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReflectionIT.Analyzer {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ReflectionIT.Analyzer.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Async methods should be named with an Async suffix.
        /// </summary>
        internal static string AsyncMethodNameSuffixAnalyzerDescription {
            get {
                return ResourceManager.GetString("AsyncMethodNameSuffixAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Async &apos;{0}&apos; method should be named with an Async suffix.
        /// </summary>
        internal static string AsyncMethodNameSuffixAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("AsyncMethodNameSuffixAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Async method should be named with an Async suffix.
        /// </summary>
        internal static string AsyncMethodNameSuffixAnalyzerTitle {
            get {
                return ResourceManager.GetString("AsyncMethodNameSuffixAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loop variables possibly incorrectly captured in anonymous method (or lambda) inside a loop body.
        /// </summary>
        internal static string ForClosureAnalyzerDescription {
            get {
                return ResourceManager.GetString("ForClosureAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Variable &apos;{0}&apos; possibly incorrectly captured in anonymous method (or lambda) inside a loop body.
        /// </summary>
        internal static string ForClosureAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("ForClosureAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Capture using local variable.
        /// </summary>
        internal static string ForClosureAnalyzerTitle {
            get {
                return ResourceManager.GetString("ForClosureAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Local variables must be camelCased.
        /// </summary>
        internal static string LocalVariableAnalyzerDescription {
            get {
                return ResourceManager.GetString("LocalVariableAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Local variable &apos;{0}&apos; is not camelCased.
        /// </summary>
        internal static string LocalVariableAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("LocalVariableAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Local variable must be camelCased.
        /// </summary>
        internal static string LocalVariableAnalyzerTitle {
            get {
                return ResourceManager.GetString("LocalVariableAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use explicit modifiers.
        /// </summary>
        internal static string MissingModifiersAnalyzerDescription {
            get {
                return ResourceManager.GetString("MissingModifiersAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; does not have an explicit modifier.
        /// </summary>
        internal static string MissingModifiersAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("MissingModifiersAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use explicit accesibility modifiers.
        /// </summary>
        internal static string MissingModifiersAnalyzerTitle {
            get {
                return ResourceManager.GetString("MissingModifiersAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non Private fields (public, protected, internal) are not alloweded, use Auto Property instead.
        /// </summary>
        internal static string NonPrivateFieldAnalyzerDescription {
            get {
                return ResourceManager.GetString("NonPrivateFieldAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non Private field &apos;{0}&apos; is not alloweded, use Auto Property instead.
        /// </summary>
        internal static string NonPrivateFieldAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("NonPrivateFieldAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non Private fields (public, protected, internal) are not alloweded.
        /// </summary>
        internal static string NonPrivateFieldAnalyzerTitle {
            get {
                return ResourceManager.GetString("NonPrivateFieldAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non Private field must be PascalCased.
        /// </summary>
        internal static string NonPrivateFieldNameAnalyzerDescription {
            get {
                return ResourceManager.GetString("NonPrivateFieldNameAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non private field &apos;{0}&apos; is not PascalCased.
        /// </summary>
        internal static string NonPrivateFieldNameAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("NonPrivateFieldNameAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non Private field must be PascalCased.
        /// </summary>
        internal static string NonPrivateFieldNameAnalyzerTitle {
            get {
                return ResourceManager.GetString("NonPrivateFieldNameAnalyzerTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private fields must start with an underscore and camelCased.
        /// </summary>
        internal static string PrivateFieldAnalyzerDescription {
            get {
                return ResourceManager.GetString("PrivateFieldAnalyzerDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private field &apos;{0}&apos; does not start with an underscore and camelCased.
        /// </summary>
        internal static string PrivateFieldAnalyzerMessageFormat {
            get {
                return ResourceManager.GetString("PrivateFieldAnalyzerMessageFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Private field must start with an underscore and camelCased.
        /// </summary>
        internal static string PrivateFieldAnalyzerTitle {
            get {
                return ResourceManager.GetString("PrivateFieldAnalyzerTitle", resourceCulture);
            }
        }
    }
}
