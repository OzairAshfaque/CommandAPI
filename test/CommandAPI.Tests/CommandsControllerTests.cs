using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandAPI.Models;
using CommandAPI.Data;
using CommandAPI.Profiles;
using CommandAPI.Dtos;
using Xunit;
using CommandAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace CommandAPI.Tests
{
public class CommandsControllerTests : IDisposable
{
    Mock<ICommandAPIRepo> mockRepo;
    CommandProfile realProfile;
    MapperConfiguration configuration;
    IMapper mapper;

    public CommandsControllerTests()
    {
        mockRepo = new Mock<ICommandAPIRepo>();
        realProfile = new CommandProfile();
        configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(realProfile));
        mapper = new Mapper(configuration);

    }

    public void Dispose()
    {
        mockRepo = null;
        mapper = null;
        configuration = null;
        realProfile = null;
    }
//GetAllCommands Unit Tests - Test 1  
[Fact]
//          <method name>_<expected result>_<condition>
public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
{
//Arrange
//We need to create an instance of our CommandsController class
//var controller = new CommandsController( /* repository, AutoMapper */);
}
[Fact]
//          <method name>_<expected result>_<condition>
public void GetCommandItems_Returns200k_WhenDBIsEmpty()
{
    //Arrange

    //var mockRepo = new Mock<ICommandAPIRepo>();
    mockRepo.Setup(repo =>
        repo.GetAllCommands()).Returns(GetCommands(0));

    //var realProfile = new CommandProfile();
    //var configuration = new MapperConfiguration(cfg =>
      //  cfg.AddProfile(realProfile));
    //IMapper mapper = new Mapper(configuration);
    var controller = new CommandsController(/* repository*/ mockRepo.Object,
          /*AutoMapper*/ mapper);

    //Act
    var result = controller.GetAllCommands();

    //Assert
    Assert.IsType<OkObjectResult>(result.Result);
}
[Fact]
 //         <method name>_<expected result>_<condition>
public void GetAllCommands_ReturnOneItem_WhenDBHasOneResource()
{
    //Arrange
    mockRepo.Setup(repo =>
        repo.GetAllCommands()).Returns(GetCommands(1));
    var controller = new CommandsController(mockRepo.Object, mapper);

    //Act
    var result = controller.GetAllCommands();

    //Assert
    var okResult = result.Result as OkObjectResult;

    var commands = okResult.Value as List<CommandReadDto>;

    Assert.Single(commands);
}

[Fact]
 //         <method name>_<expected result>_<condition>
public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
{
    //Arrange
    mockRepo.Setup(repo =>
        repo.GetAllCommands()).Returns(GetCommands(1));
    var controller = new CommandsController(mockRepo.Object, mapper);

    //Act 
    var result = controller.GetAllCommands();

    //Assert
    Assert.IsType<OkObjectResult>(result.Result);
}

//GetCommandByID - Test 2

//Resource ID is invalid (does not exist in DB) 404 Not Found HTTP Response
[Fact]
 //         <method name>_<expected result>_<condition>
public void GetCommandByID_Returns404NotFound_WhenNonExistentIDProvided()
{
    //Arrange
    mockRepo.Setup(repo =>
        repo.GetCommandById(0)).Returns(() => null);

    var controller = new CommandsController(mockRepo.Object, mapper);

    //Act    
    var result = controller.GetCommandById(1);

    //Assert
    Assert.IsType<NotFoundResult>(result.Result);

}

//Resource ID is valid (exists in the DB) 200 Ok HTTP Response
[Fact]
 //         <method name>_<expected result>_<condition>
 public void GetCommandByID_Returns200OK__WhenValidIDProvided()
 {
     //Arrange
     mockRepo.Setup(repo =>
        repo.GetCommandById(1)).Returns(new Command { Id = 1,
        HowTo = "mock",
        Platform = "Mock",
        CommandLine = "Mock" });

    var controller = new CommandsController(mockRepo.Object, mapper);

    //Act
    var result = controller.GetCommandById(1);

    //Assert
    Assert.IsType<OkObjectResult>(result.Result);
 }

//Resource ID is valid (exists in the DB) Correct Resource Type Returned
[Fact]
//         <method name>_<expected result>_<condition>
public void GetCommandByID_Returns200OKForCorrectObjectType__WhenValidIDProvided()
{
    mockRepo.Setup(repo =>
        repo.GetCommandById(1)).Returns(new Command { Id = 1,
        HowTo = "mock",
        Platform = "Mock",
        CommandLine = "Mock" });

    var controller = new CommandsController(mockRepo.Object, mapper);

    //Act
    var result = controller.GetCommandById(1);

    //Assert
    Assert.IsType<ActionResult<CommandReadDto>>(result);

}

private List<Command> GetCommands(int num)
{
    var commands = new List<Command>();
    if (num > 0)
    {
        commands.Add(new Command
        {
            Id = 0,
            HowTo = "Hot to generate a Migration",
            CommandLine = "dotnet ef migration add <Name of Migration>",
            Platform = ".Net Core EF"
        });

    }

    return commands;

}



}
}