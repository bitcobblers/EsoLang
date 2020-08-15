using System.Collections.Generic;

namespace EsoLang.Brainfuck
{
    public class BfInterpreter : Interpreter
    {
        public override string Delimiter => string.Empty;

        public override Dictionary<string, Op> Map => new Dictionary<string, Op>
        {
            [">"] = Op.IncPointer,
            ["<"] = Op.DecPointer,
            ["+"] = Op.IncValue,
            ["-"] = Op.DecValue,
            ["."] = Op.PutChar,
            [","] = Op.GetChar,
            ["["] = Op.LoopStart,
            ["]"] = Op.LoopEnd
        };

        public override IEnumerable<Op> Tokenize(string source)
        {
            if(string.IsNullOrWhiteSpace(source))
            {
                yield break;
            }

            foreach(var c in source)
            {
                if(char.IsWhiteSpace(c))
                {
                    continue;
                }

                if(this.Map.TryGetValue(c.ToString(), out var token))
                {
                    yield return token;
                }
                else
                {
                    throw new UnrecognizedTokenException(c);
                }
            }
        }
    }
}