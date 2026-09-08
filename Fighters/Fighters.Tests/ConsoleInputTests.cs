using Fighters.ConsoleUi;
using Moq;

namespace Fighters.Tests;

public class ConsoleInputTests
{
    private static Mock<IConsole> CreateConsoleMock( params string?[] inputs )
    {
        Mock<IConsole> console = new Mock<IConsole>();
        Queue<string?> queue = new Queue<string?>( inputs );
        console.Setup( c => c.ReadLine() ).Returns( () => queue.Count > 0 ? queue.Dequeue() : null );
        return console;
    }

    [Fact]
    public void ReadName_ValidName_ReturnsName()
    {
        Mock<IConsole> console = CreateConsoleMock( "Боец" );
        ConsoleInput input = new ConsoleInput( console.Object );

        string name = input.ReadName( "Имя:" );

        Assert.Equal( "Боец", name );
    }

    [Fact]
    public void ReadName_EmptyThenValid_ReturnsValidName()
    {
        Mock<IConsole> console = CreateConsoleMock( "  ", "Боец" );
        ConsoleInput input = new ConsoleInput( console.Object );

        string name = input.ReadName( "Имя:" );

        Assert.Equal( "Боец", name );
    }

    [Fact]
    public void ReadEnumOption_ValidIndex_ReturnsEnumValue()
    {
        Mock<IConsole> console = CreateConsoleMock( "1" );
        ConsoleInput input = new ConsoleInput( console.Object );

        TestEnum value = input.ReadEnumOption<TestEnum>( "Выберите:", new List<string> { "A", "B", "C" } );

        Assert.Equal( TestEnum.B, value );
    }

    [Fact]
    public void ReadEnumOption_InvalidThenValid_ReturnsValidValue()
    {
        Mock<IConsole> console = CreateConsoleMock( "9", "abc", "2" );
        ConsoleInput input = new ConsoleInput( console.Object );

        TestEnum value = input.ReadEnumOption<TestEnum>( "Выберите:", new List<string> { "A", "B", "C" } );

        Assert.Equal( TestEnum.C, value );
    }

    private enum TestEnum
    {
        A,
        B,
        C
    }
}