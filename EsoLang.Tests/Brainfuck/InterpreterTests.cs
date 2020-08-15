using EsoLang.Brainfuck;
using FakeItEasy;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace EsoLang.Tests.Brainfuck
{
    public class InterpreterTests
    {
        private readonly ITestOutputHelper output;

        public InterpreterTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        public class StringifyMethod : InterpreterTests
        {
            public StringifyMethod(ITestOutputHelper output) : base(output)
            {
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void NullOrEmptySourceReturnsEmptyString(string source)
            {
                // Arrange.
                var interpreter = A.Fake<Interpreter>();

                A.CallTo(() => interpreter.Delimiter).Returns(string.Empty);
                A.CallTo(() => interpreter.Map).Returns(new Dictionary<string, Op>());
                A.CallTo(() => interpreter.Stringify(A<string>._)).CallsBaseMethod();

                // Act.
                var result = interpreter.Stringify(source);

                // Assert.
                Assert.Equal(string.Empty, result);
            }
        }

        public class ShiftPointerMethod : InterpreterTests
        {
            public ShiftPointerMethod(ITestOutputHelper output) : base(output)
            {
            }
        }

        public class GetSignMethod : InterpreterTests
        {
            public GetSignMethod(ITestOutputHelper output) : base(output)
            {
            }

            [Theory]
            [InlineData(-1, -1)]
            [InlineData(0, 0)]
            [InlineData(1,1 )]
            public void Scenarios(int value, int expected)
            {
                // Act.
                var result = Interpreter.GetSign(value);

                // Assert.
                Assert.Equal(expected, result);
            }
        }

        public class GetNearestMethod : InterpreterTests
        {
            public GetNearestMethod(ITestOutputHelper output) : base(output)
            {
            }

            [Theory]
            [InlineData(0, 0, new byte[] { 0 })]
            [InlineData(125, 2, new byte[] { 0, 50, 100 })]
            [InlineData(60, 1, new byte[] { 0, 50, 100 })]
            public void Scenarios(byte search, int expected, byte[] buffer)
            {
                // Act.
                var result = Interpreter.GetNearest(buffer, search);

                // Assert.
                Assert.Equal(expected, result);
            }
        }
    }
}