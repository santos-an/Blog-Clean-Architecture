using Application.Interfaces;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Queries.GetAllPosts;
using Application.Posts.Queries.GetPost;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application.Tests.Posts.Commands;

public class CreatePostCommandTests
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly CreatePostCommand _command;

    public CreatePostCommandTests()
    {
        _mapper = new Mock<IMapper>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _command = new CreatePostCommand(_mapper.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Execute_GetsCalled_CreatesNewPostAndCommitsChanges()
    {
        // arrange
        var input = new CreatePostDto { Comments = new List<CreateCommentDto>() };
        _unitOfWork.Setup(u => u.Posts.Create(It.IsAny<Post>()));
        _mapper.Setup(m => m.Map<PostDto>(It.IsAny<Post>())).Returns(new PostDto());

        // act
        await _command.Execute(input);

        // assert
        _unitOfWork.Verify(u => u.Posts.Create(It.IsAny<Post>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(), Times.Once);
        _mapper.Verify(m => m.Map<PostDto>(It.IsAny<Post>()), Times.Once);
    }
}