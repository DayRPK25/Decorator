// Este archivo lo hizo la IA

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ScriptDecoratorApp
{
    // ── Modelo de token ───────────────────────────────────────────────────────
    public enum ScriptTokenKind
    {
        Keyword, String, Comment, Number,
        Decorator, Builtin, Function, Plain
    }

    public record ScriptToken(ScriptTokenKind Kind, string Text);

    // ── FormattedScript (Decorator) ───────────────────────────────────────────
    // Añade syntax highlight al script base. Funciona en dos modos:
    //   • GetHighlightedHtml()  → para WebView / HTML
    //   • GetTokens()           → para WinForms RichTextBox
    public class FormattedScript : ScriptDecorator
    {
        public FormattedScript(IScript inner) : base(inner) { }

        // ── API para WinForms ────────────────────────────────────────────────
        public List<ScriptToken> GetTokens()
        {
            return Tokenize(GetText());
        }

        // ── API para HTML ────────────────────────────────────────────────────
        public string getHighlightedHtml()
        {
            var tokens = Tokenize(GetText());
            var sb = new System.Text.StringBuilder();
            sb.Append("<pre style='background:#1e1e1e;font-family:Consolas;font-size:13px;padding:16px'>");
            foreach (var t in tokens)
            {
                string color = t.Kind switch
                {
                    ScriptTokenKind.Keyword   => "#569CD6",
                    ScriptTokenKind.String    => "#CE9178",
                    ScriptTokenKind.Comment   => "#6A9955",
                    ScriptTokenKind.Number    => "#B5CEA8",
                    ScriptTokenKind.Decorator => "#DCDCAA",
                    ScriptTokenKind.Builtin   => "#4EC9B0",
                    ScriptTokenKind.Function  => "#DCDCAA",
                    _                         => "#D4D4D4"
                };
                string escaped = System.Web.HttpUtility.HtmlEncode(t.Text);
                sb.Append($"<span style='color:{color}'>{escaped}</span>");
            }
            sb.Append("</pre>");
            return sb.ToString();
        }

        // ── Tokenizer ────────────────────────────────────────────────────────
        private static readonly (ScriptTokenKind Kind, Regex Re)[] Rules =
        [
            (ScriptTokenKind.Comment,
                new Regex(@"#[^\n]*", RegexOptions.Compiled)),

            (ScriptTokenKind.String,
                new Regex(@"(""""""[\s\S]*?""""""|'''[\s\S]*?'''|""[^""\n]*""|'[^'\n]*')",
                          RegexOptions.Compiled)),

            (ScriptTokenKind.Decorator,
                new Regex(@"@\w+", RegexOptions.Compiled)),

            (ScriptTokenKind.Keyword,
                new Regex(@"\b(def|class|import|from|return|if|elif|else|for|while|"
                         + @"in|not|and|or|is|None|True|False|try|except|finally|"
                         + @"with|as|pass|break|continue|lambda|yield|async|await|"
                         + @"raise|del|global|nonlocal)\b", RegexOptions.Compiled)),

            (ScriptTokenKind.Builtin,
                new Regex(@"\b(print|len|range|type|int|str|float|list|dict|tuple|"
                         + @"set|bool|open|input|super|self|cls|enumerate|zip|map|"
                         + @"filter|sorted|reversed|isinstance|hasattr|getattr|setattr)\b",
                          RegexOptions.Compiled)),

            (ScriptTokenKind.Number,
                new Regex(@"\b\d+\.?\d*\b", RegexOptions.Compiled)),

            (ScriptTokenKind.Function,
                new Regex(@"\b(\w+)(?=\s*\()", RegexOptions.Compiled)),
        ];

        private static List<ScriptToken> Tokenize(string code)
        {
            // Recopilar todos los matches con su posición
            var spans = new List<(int Start, int End, ScriptTokenKind Kind)>();

            foreach (var (kind, re) in Rules)
            {
                foreach (Match m in re.Matches(code))
                {
                    // Descartar si se solapa con un span ya aceptado
                    bool overlaps = false;
                    foreach (var s in spans)
                        if (m.Index < s.End && m.Index + m.Length > s.Start)
                        { overlaps = true; break; }

                    if (!overlaps)
                        spans.Add((m.Index, m.Index + m.Length, kind));
                }
            }

            spans.Sort((a, b) => a.Start.CompareTo(b.Start));

            // Construir lista final intercalando texto plano
            var tokens = new List<ScriptToken>();
            int cursor = 0;
            foreach (var (start, end, kind) in spans)
            {
                if (cursor < start)
                    tokens.Add(new ScriptToken(ScriptTokenKind.Plain, code[cursor..start]));
                tokens.Add(new ScriptToken(kind, code[start..end]));
                cursor = end;
            }
            if (cursor < code.Length)
                tokens.Add(new ScriptToken(ScriptTokenKind.Plain, code[cursor..]));

            return tokens;
        }
    }
}
