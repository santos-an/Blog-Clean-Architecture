![build](https://github.com/santos-an/devskiller-code-WT97-TAH7-TY6E-S0F/actions/workflows/build.yml/badge.svg)
![test](https://github.com/santos-an/devskiller-code-WT97-TAH7-TY6E-S0F/actions/workflows/test.yml/badge.svg)

## Introduction
This is a sample application, to demonstrate a Rest Api implementation, using **Clean Architecture** and Microsoft .NET 6. 


### Context
> You were hired as a consultant for BestBlogs<sup>TM</sup> company. The company needs your help to implement a REST api for its newest blog product.

### Domain
```csharp
public class Comment
{
  public Guid Id { get; set; }
  public Post Post { get; set; }
  public Guid PostId { get; set; }
  public string Content { get; set; }
  public string Author { get; set; }
  public DateTime CreationDate { get; set; }
}
```

```csharp
public class Post
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Content { get; set; }
  public DateTime CreationDate { get; set; }
  public List<Comment> Comments { get; set;  } = new();
}
```


### Infrastructure layer
We have 2 repositories, and we are using the unit of work pattern:

- `CommentRepository.GetAll` - finds all existing comments
- `CommentRepository.Get` - find the comment by id
- `CommentRepository.Create` - inserts a given comment
- `CommentRepository.Update` - updates a given comment
- `CommentRepository.Delete` - deletes a given comment
- `CommentRepository.GetByPostId` - finds all comments by post id

- `PostRepository.GetAll` - finds all existing posts
- `PostRepository.Get` - finds the post by id
- `PostRepository.Create` - inserts a given post
- `PostRepository.Update` - updates a given comment
- `PostRepository.Delete` - deletes a given comment

### Api layer

**Comment endpoints**

- GET `/comments` - returns all comments
- GET `/comments/{id}` - returns comment by id
- POST `/comments` - creates a new comment
- PUT `/comments` - updates a comment
- DELETE `/comments/{id}` - deletes a comment by id

**Post endpoints**

- GET `/posts` - returns all posts
- GET `/posts/{id}` - returns post by id
- POST `/posts` - creates a new post
- PUT `/posts` - updates a post
- DELETE `/posts/{id}` - deletes a post by id
- GET `/posts/{id}/comments` - returns all comments for a given post id

#### Request validations
**Comment**

- `PostId` must be an existing post id
- `Content` should not have more than 120 characters
- `Author` should not have more than 30 characters

**Post**

- `Title` should not have more than 30 characters
- `Content` should not have more than 1200 characters

## Technologies
This demo application uses the following technologies:
 - .NET 6
 - C# 10
 - ASP.NET Core MVC 6.0
 - EF Core 6.0
 - Rider 2022
 - SQL Server 2022
 - XUnit 2.4
 - Moq 4
 - Fluent Validation 11.2

## Docker (on windows containers)
- `docker build -t blog -f Dockerfile .`
- `docker run -it --name blog -p 8081:80 -d blog`
