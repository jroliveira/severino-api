namespace Severino.WebApi.Features.Client
{
    using System.ComponentModel.DataAnnotations;

    using Severino.Domain.Client;
    using Severino.Infrastructure.Monad;

    using static Severino.Domain.Client.Client;

    public class UpdateClientModel
    {
        public UpdateClientModel(string name) => this.Name = name;

        [Required]
        public string Name { get; }

        public Option<Client> ToEntity(string id) => NewClient(id, this.Name);
    }
}
