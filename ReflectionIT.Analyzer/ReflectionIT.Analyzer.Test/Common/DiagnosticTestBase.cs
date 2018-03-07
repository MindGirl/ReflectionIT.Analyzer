using System;
using System.Linq;
//using NUnit.Framework;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Threading;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.Host.Mef;
using System.Text;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionIT.Analyzer.Analyzers;

namespace RefactoringEssentials.Tests
{
    public abstract class DiagnosticTestBase
    {
        static MetadataReference _mscorlib;
        static MetadataReference _systemAssembly;
        static MetadataReference _systemXmlLinq;
        static MetadataReference _systemCore;

        internal static MetadataReference[] DefaultMetadataReferences;

        static Dictionary<string, CodeFixProvider> _providers = new Dictionary<string, CodeFixProvider>();

        static DiagnosticTestBase()
        {
            try
            {
                _mscorlib = MetadataReference.CreateFromFile(typeof(Console).Assembly.Location);
                _systemAssembly = MetadataReference.CreateFromFile(typeof(System.ComponentModel.BrowsableAttribute).Assembly.Location);
                _systemXmlLinq = MetadataReference.CreateFromFile(typeof(System.Xml.Linq.XElement).Assembly.Location);
                _systemCore = MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);
                DefaultMetadataReferences = new[] {
                    _mscorlib,
                    _systemAssembly,
                    _systemCore,
                    _systemXmlLinq
                };

                foreach (var provider in typeof(DiagnosticAnalyzerCategories).Assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(ExportCodeFixProviderAttribute), false).Length > 0))
                {
                    //var attr = (ExportCodeFixProviderAttribute)provider.GetCustomAttributes(typeof(ExportCodeFixProviderAttribute), false) [0];
                    var codeFixProvider = (CodeFixProvider)Activator.CreateInstance(provider);
                    foreach (var id in codeFixProvider.FixableDiagnosticIds)
                    {
                        if (_providers.ContainsKey(id))
                        {
                            Console.WriteLine("Provider " + id + " already added.");
                            continue;
                        }
                        _providers.Add(id, codeFixProvider);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string GetUniqueName()
        {
            return Guid.NewGuid().ToString("D");
        }

        internal class TestWorkspace : Workspace
        {
            readonly static HostServices _services = Microsoft.CodeAnalysis.Host.Mef.MefHostServices.DefaultHost;/* MefHostServices.Create(new [] { 
				typeof(MefHostServices).Assembly,
				typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions).Assembly
			});*/


            public TestWorkspace(string workspaceKind = "Test") : base(_services, workspaceKind)
            {
                /*
                foreach (var a in MefHostServices.DefaultAssemblies)
                {
                    Console.WriteLine(a.FullName);
                }*/
            }

            public void ChangeDocument(DocumentId id, SourceText text)
            {
                ApplyDocumentTextChanged(id, text);
            }

            protected override void ApplyDocumentTextChanged(DocumentId id, SourceText text)
            {
                base.ApplyDocumentTextChanged(id, text);
                var document = CurrentSolution.GetDocument(id);
                if (document != null) {
                    OnDocumentTextChanged(id, text, PreservationMode.PreserveValue);
                }
            }

            public override bool CanApplyChange(ApplyChangesKind feature)
            {
                return true;
            }

            public void Open(ProjectInfo projectInfo)
            {
                var sInfo = SolutionInfo.Create(
                                SolutionId.CreateNewId(),
                                VersionStamp.Create(),
                                null,
                                new[] { projectInfo }
                            );
                OnSolutionAdded(sInfo);
            }
        }

        protected static void RunFix(Workspace workspace, ProjectId projectId, DocumentId documentId, Diagnostic diagnostic, int index = 0)
        {
            CodeFixProvider provider;
            if (_providers.TryGetValue(diagnostic.Id, out provider))
            {
                Assert.IsNotNull(provider, "null provider for : " + diagnostic.Id);
                var document = workspace.CurrentSolution.GetProject(projectId).GetDocument(documentId);
                var actions = new List<CodeAction>();
                var context = new CodeFixContext(document, diagnostic, (fix, diags) => actions.Add(fix), default(CancellationToken));
                provider.RegisterCodeFixesAsync(context).Wait();
                if (!actions.Any())
                {
                    Assert.Fail("Provider has no fix for " + diagnostic.Id + " at " + diagnostic.Location.SourceSpan);
                    return;
                }
                foreach (var op in actions[index].GetOperationsAsync(default(CancellationToken)).Result)
                {
                    op.Apply(workspace, default(CancellationToken));
                }
            }
            else
            {
                Assert.Fail("No code fix provider found for :" + diagnostic.Id);
            }
        }

        protected static void Test<T>(string input, int expectedDiagnostics = 1, string output = null, int issueToFix = -1, int actionToRun = 0) where T : DiagnosticAnalyzer, new()
        {
            Assert.Fail("Use Analyze");
        }

        protected static void Test<T>(string input, string output, int fixIndex = 0)
            where T : DiagnosticAnalyzer, new()
        {
            Assert.Fail("Use Analyze");
        }

        protected static void TestIssue<T>(string input, int issueCount = 1)
            where T : DiagnosticAnalyzer, new()
        {
            Assert.Fail("Use Analyze");
        }

        protected static void TestWrongContextWithSubIssue<T>(string input, string id) where T : DiagnosticAnalyzer, new()
        {
            Assert.Fail("Use AnalyzeWithRule");
        }

        protected static void TestWithSubIssue<T>(string input, string output, string subIssue, int fixIndex = 0) where T : DiagnosticAnalyzer, new()
        {
            Assert.Fail("Use AnalyzeWithRule");
        }

        class TestDiagnosticAnalyzer<T> : DiagnosticAnalyzer
        {
            readonly DiagnosticAnalyzer _t;

            public TestDiagnosticAnalyzer(DiagnosticAnalyzer t)
            {
                this._t = t;
            }

            #region IDiagnosticAnalyzer implementation
            public override void Initialize(AnalysisContext context)
            {
                _t.Initialize(context);
            }

            public override System.Collections.Immutable.ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            {
                get
                {
                    return _t.SupportedDiagnostics;
                }
            }
            #endregion
        }

        protected static TextSpan GetWholeSpan(Diagnostic d)
        {
            int start = d.Location.SourceSpan.Start;
            int end = d.Location.SourceSpan.End;
            foreach (var a in d.AdditionalLocations)
            {
                start = Math.Min(start, a.SourceSpan.Start);
                end = Math.Max(start, a.SourceSpan.End);
            }
            return TextSpan.FromBounds(start, end);
        }

        protected static void Analyze<T>(Func<string, SyntaxTree> parseTextFunc, Func<SyntaxTree[], Compilation> createCompilationFunc, string language, string input, string output = null, int issueToFix = -1, int actionToRun = 0, Action<int, Diagnostic> diagnosticCheck = null) where T : DiagnosticAnalyzer, new()
        {
            var text = new StringBuilder();

            var expectedDiagnosics = new List<TextSpan>();
            int start = -1;
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch == '$')
                {
                    if (start < 0)
                    {
                        start = text.Length;
                        continue;
                    }
                    expectedDiagnosics.Add(TextSpan.FromBounds(start, text.Length));
                    start = -1;
                }
                else
                {
                    text.Append(ch);
                }
            }

            var syntaxTree = parseTextFunc(text.ToString());

            Compilation compilation = createCompilationFunc(new[] { syntaxTree });

            var diagnostics = new List<Diagnostic>();

            var compilationWithAnalyzers = compilation.WithAnalyzers(System.Collections.Immutable.ImmutableArray<DiagnosticAnalyzer>.Empty.Add(new T()));
            var result = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;
            diagnostics.AddRange(result);

            if (expectedDiagnosics.Count != diagnostics.Count)
            {
                foreach (var diag in diagnostics)
                {
                    Console.WriteLine(diag.Id + "/" + diag.GetMessage() + "/" + diag.Location.SourceSpan);
                }
                Assert.Fail("Diagnostic count mismatch expected: " + expectedDiagnosics.Count + " was " + diagnostics.Count);
            }

            for (int i = 0; i < expectedDiagnosics.Count; i++)
            {
                var d = diagnostics[i];
                var wholeSpan = GetWholeSpan(d);
                if (wholeSpan != expectedDiagnosics[i])
                {
                    Assert.Fail("Diagnostic " + i + " span mismatch expected: " + expectedDiagnosics[i] + " but was " + wholeSpan);
                }
                if (diagnosticCheck != null) {
                    diagnosticCheck(i, d);
                }
            }

            if (output == null) {
                return;
            }

            var workspace = new TestWorkspace();
            var projectId = ProjectId.CreateNewId();
            var documentId = DocumentId.CreateNewId(projectId);
            workspace.Open(ProjectInfo.Create(
                projectId,
                VersionStamp.Create(),
                "a", "a.exe", language, null, null, null, null,
                new[] {
                    DocumentInfo.Create(
                        documentId,
                        "a.cs",
                        null,
                        SourceCodeKind.Regular,
                        TextLoader.From(TextAndVersion.Create(SourceText.From(text.ToString()), VersionStamp.Create())))
                }
            ));
            if (issueToFix < 0)
            {
                diagnostics.Reverse();
                foreach (var v in diagnostics)
                {
                    RunFix(workspace, projectId, documentId, v);
                }
            }
            else
            {
                RunFix(workspace, projectId, documentId, diagnostics.ElementAt(issueToFix), actionToRun);
            }

            var txt = workspace.CurrentSolution.GetProject(projectId).GetDocument(documentId).GetTextAsync().Result.ToString();
            output = CodeFixTestBase.HomogenizeEol(output);
            txt = CodeFixTestBase.HomogenizeEol(txt);
            if (output != txt)
            {
                Console.WriteLine("expected:");
                Console.WriteLine(output);
                Console.WriteLine("got:");
                Console.WriteLine(txt);
                Console.WriteLine("-----Mismatch:");
                for (int i = 0; i < txt.Length; i++)
                {
                    if (i >= output.Length)
                    {
                        Console.Write("#");
                        continue;
                    }
                    if (txt[i] != output[i])
                    {
                        Console.Write("#");
                        continue;
                    }
                    Console.Write(txt[i]);
                }
                Assert.Fail();
            }
        }

        protected static void AnalyzeWithRule<T>(Func<string, SyntaxTree> parseTextFunc, Func<SyntaxTree[], Compilation> createCompilationFunc, string language, string input, string ruleId, string output = null, int issueToFix = -1, int actionToRun = 0, Action<int, Diagnostic> diagnosticCheck = null) where T : DiagnosticAnalyzer, new()
        {
            var text = new StringBuilder();

            var expectedDiagnosics = new List<TextSpan>();
            int start = -1;
            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (ch == '$')
                {
                    if (start < 0)
                    {
                        start = text.Length;
                        continue;
                    }
                    expectedDiagnosics.Add(TextSpan.FromBounds(start, text.Length));
                    start = -1;
                }
                else
                {
                    text.Append(ch);
                }
            }

            var syntaxTree = parseTextFunc(text.ToString());

            Compilation compilation = createCompilationFunc(new[] { syntaxTree });

            var diagnostics = new List<Diagnostic>();
            var compilationWithAnalyzers = compilation.WithAnalyzers(System.Collections.Immutable.ImmutableArray<DiagnosticAnalyzer>.Empty.Add(new T()));
            diagnostics.AddRange(compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result);


            if (expectedDiagnosics.Count != diagnostics.Count)
            {
                Console.WriteLine("Diagnostics: " + diagnostics.Count);
                foreach (var diag in diagnostics)
                {
                    Console.WriteLine(diag.Id + "/" + diag.GetMessage());
                }
                Assert.Fail("Diagnostic count mismatch expected: " + expectedDiagnosics.Count + " but was:" + diagnostics.Count);
            }

            for (int i = 0; i < expectedDiagnosics.Count; i++)
            {
                var d = diagnostics[i];
                var wholeSpan = GetWholeSpan(d);
                if (wholeSpan != expectedDiagnosics[i])
                {
                    Assert.Fail("Diagnostic " + i + " span mismatch expected: " + expectedDiagnosics[i] + " but was " + wholeSpan);
                }
                if (diagnosticCheck != null) {
                    diagnosticCheck(i, d);
                }
            }

            if (output == null) {
                return;
            }

            var workspace = new TestWorkspace();
            var projectId = ProjectId.CreateNewId();
            var documentId = DocumentId.CreateNewId(projectId);
            workspace.Open(ProjectInfo.Create(
                projectId,
                VersionStamp.Create(),
                "", "", language, null, null, null, null,
                new[] {
                    DocumentInfo.Create(
                        documentId,
                        "a.cs",
                        null,
                        SourceCodeKind.Regular,
                        TextLoader.From(TextAndVersion.Create(SourceText.From(text.ToString()), VersionStamp.Create())))
                }
            ));
            if (issueToFix < 0)
            {
                diagnostics.Reverse();
                foreach (var v in diagnostics)
                {
                    RunFix(workspace, projectId, documentId, v);
                }
            }
            else
            {
                RunFix(workspace, projectId, documentId, diagnostics.ElementAt(issueToFix), actionToRun);
            }

            var txt = workspace.CurrentSolution.GetProject(projectId).GetDocument(documentId).GetTextAsync().Result.ToString();
            txt = CodeFixTestBase.HomogenizeEol(txt);
            output = CodeFixTestBase.HomogenizeEol(output);
            if (output != txt)
            {
                Console.WriteLine("expected:");
                Console.WriteLine(output);
                Console.WriteLine("got:");
                Console.WriteLine(txt);
                Assert.Fail();
            }
        }
    }
}

