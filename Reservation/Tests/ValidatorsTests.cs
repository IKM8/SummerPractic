using Domain.Exceptions;
using Infrastructure.Foundation.Services;

namespace Reservation.Tests;

public class ValidatorsTests
{
    [Fact]
    public void ValidatePrice_Negative_Throws()
    {
        Assert.Throws<BusinessRuleViolationException>(
            () => Validators.ValidatePrice( -100m ) );
    }

    [Fact]
    public void ValidatePrice_Zero_DoesNotThrow()
    {
        var exception = Record.Exception( () => Validators.ValidatePrice( 0m ) );
        Assert.Null( exception );
    }

    [Fact]
    public void ValidatePrice_Positive_DoesNotThrow()
    {
        var exception = Record.Exception( () => Validators.ValidatePrice( 5000m ) );
        Assert.Null( exception );
    }

    [Theory]
    [InlineData( -1, 5 )]
    [InlineData( 5, -1 )]
    [InlineData( 5, 3 )]
    public void ValidatePersonCount_Invalid_Throws( int min, int max )
    {
        Assert.Throws<BusinessRuleViolationException>(
            () => Validators.ValidatePersonCount( min, max ) );
    }

    [Fact]
    public void ValidatePersonCount_Valid_DoesNotThrow()
    {
        var exception = Record.Exception( () => Validators.ValidatePersonCount( 1, 5 ) );
        Assert.Null( exception );
    }

    [Fact]
    public void ValidateAvailableRooms_Negative_Throws()
    {
        Assert.Throws<BusinessRuleViolationException>(
            () => Validators.ValidateAvailableRooms( -1 ) );
    }

    [Fact]
    public void ValidateAvailableRooms_Zero_DoesNotThrow()
    {
        var exception = Record.Exception( () => Validators.ValidateAvailableRooms( 0 ) );
        Assert.Null( exception );
    }

    [Theory]
    [InlineData( "ЭтотНазваниеОтельОченьДлинноеИПревышаетСтоСимволовПотомуЧтоЭтоТестВалидацииДляПроверкиПравилДлиныСтрокИПоляНазвания", 100 )]
    public void ValidateStringLength_TooLong_Throws( string value, int maxLength )
    {
        string fieldName = "TestField";
        Assert.Throws<BusinessRuleViolationException>(
            () => Validators.ValidateStringLength( value, fieldName, maxLength ) );
    }

    [Theory]
    [InlineData( "OK", 100 )]
    [InlineData( "", 100 )]
    public void ValidateStringLength_ShortEnough_DoesNotThrow( string value, int maxLength )
    {
        string fieldName = "TestField";
        var exception = Record.Exception( () => Validators.ValidateStringLength( value, fieldName, maxLength ) );
        Assert.Null( exception );
    }
}
