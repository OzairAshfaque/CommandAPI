using System;
using Xunit;
using CommandAPI.Models;
namespace CommandAPI.Tests
{
public class CommandTests : IDisposable
{
    Command testCommand;
    public CommandTests()
    {
       testCommand =  new Command
    {        
        HowTo = "Do Something Awesome",
        Platform = "xUnit",
        CommandLine = "dotnet test"
    }; 

    }
    public void Dispose()
    {
        testCommand = null;
    
    }
[Fact]
public void CanChangeHowTo()
{
    //Arrange
/*    var testCommand = new Command
    {        
        HowTo = "Do Something Awesome",
        Platform = "xUnit",
        CommandLine = "dotnet test"
    };
*/


    //Act
    //string changedValue = "Execute Unit Tests";
    testCommand.HowTo = "Execute Unit Tests";
    //Assert
    Assert.Equal("Execute Unit Tests",testCommand.HowTo);
    
    }
    [Fact]
    public void CanChangePlatform()
    {
        //Act
        testCommand.Platform = "New Xunit";
        //Assert
        Assert.Equal("New Xunit",testCommand.Platform);
    }
    [Fact]
    public void CanChangeCommandLine()
    {
        //Act
        testCommand.CommandLine = "New Dotnet Test";
        //Assert
        Assert.Equal("New Dotnet Test",testCommand.CommandLine);
    }
    
}
}