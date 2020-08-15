using EsoLang.Brainfuck;
using FakeItEasy;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EsoLang.Tests.Brainfuck
{
    public class BfInterpreterTests
    {
        private readonly ITestOutputHelper output;

        public BfInterpreterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class TokenizeMethod : BfInterpreterTests
        {
            public TokenizeMethod(ITestOutputHelper output) : base(output)
            {
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void NullOrEmptySourceReturnsNoTokens(string source)
            {
                // Arrange.
                var interpreter = new BfInterpreter();

                // Act.
                var result = interpreter.Tokenize(source);

                // Assert.
                Assert.Empty(result);
            }

            [Theory]
            [InlineData(">", Op.IncPointer)]
            [InlineData("<", Op.DecPointer)]
            [InlineData("+", Op.IncValue)]
            [InlineData("-", Op.DecValue)]
            [InlineData(".", Op.PutChar)]
            [InlineData(",", Op.GetChar)]
            [InlineData("[", Op.LoopStart)]
            [InlineData("]", Op.LoopEnd)]
            [InlineData("[]", Op.LoopStart, Op.LoopEnd)]
            public void ValidScenarios(string source, params Op[] expected)
            {
                // Arrange.
                var interpreter = new BfInterpreter();

                // Act.
                var result = interpreter.Tokenize(source).ToArray();

                // Assert.
                Assert.Equal(expected, result);
            }

            [Fact]
            public void UnknownCharacterThrowsUnrecognizedTokenException()
            {
                // Arrange.
                var interpreter = new BfInterpreter();

                // Act and assert.
                Assert.Throws<UnrecognizedTokenException>(() => interpreter.Tokenize("?").ToArray());
            }
        }
    }
}