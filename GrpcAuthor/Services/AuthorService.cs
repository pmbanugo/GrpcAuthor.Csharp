using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcAuthor
{
    public class AuthorService : Author.AuthorBase
    {
        private readonly ILogger<AuthorService> _logger;
        private List<AuthorResponse> authors;

        public AuthorService(ILogger<AuthorService> logger)
        {
            _logger = logger;
            authors = new List<AuthorResponse>();

            var antonio = new AuthorResponse { Name = "Antonio Gonzalez" };
            antonio.BooksAuthored.Add(new BookReply { Title = "Much Ado about nothing" });
            antonio.BooksAuthored.Add(new BookReply { Title = "How to do a split"});
            authors.Add(antonio);

            var jack = new AuthorResponse { Name = "Jack Olabisi" };
            jack.BooksAuthored.Add(new BookReply { Title = "Early morning bird" });
            jack.BooksAuthored.Add(new BookReply { Title = "Fly me to Paris"});
            authors.Add(jack);
        }

        public override Task<AuthorResponse> GetAuthor(AuthorRequest request, ServerCallContext context)
        {
            var author = authors.FirstOrDefault(x => x.Name == request.Name);
            return Task.FromResult(author);
        }
    }
}
