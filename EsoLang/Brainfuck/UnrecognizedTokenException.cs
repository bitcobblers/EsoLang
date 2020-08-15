using System;

namespace EsoLang.Brainfuck
{
    [Serializable]
    public class UnrecognizedTokenException : Exception
    {
        public char Token { get; }

        public UnrecognizedTokenException() : this('?')
        {
        }

        public UnrecognizedTokenException(char token, Exception innerException=null) : base( $"An unrecognized token was encountered while parsing source: '{token}'.", innerException)
        {
            this.Token = token;
        }
    }
}