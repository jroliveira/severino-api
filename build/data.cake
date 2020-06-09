public sealed class BuildData
{
    public BuildData(
        ICakeContext context,
        string configuration,
        Func<BuildData, Action<Exception>> errorHandler,
        RepositoryData repository,
        SolutionData solution)
    {
        this.Configuration = configuration;
        this.Repository = repository;
        this.Solution = solution;

        this.DirectoriesToDelete = context
            .GetDirectories(this.Solution.ArtifactsDirectory)
            .Concat(context.GetDirectories("./src/**/bin"))
            .Concat(context.GetDirectories("./src/**/obj"))
            .OrderBy(directory => directory.ToString())
            .ToList();

        this.ErrorHandler = errorHandler(this);
    }

    public string Configuration { get; }
    public Action<Exception> ErrorHandler { get; }
    public RepositoryData Repository { get; }
    public SolutionData Solution { get; }
    public IEnumerable<DirectoryPath> DirectoriesToDelete { get; }
}

public sealed class RepositoryData
{
    public RepositoryData(string remote) => this.Remote = remote;

    public string Remote { get; }
}

public sealed class SolutionData
{
    public SolutionData(
        string artifactsDirectory,
        string slnPath,
        string webApiPath)
    {
        this.ArtifactsDirectory = artifactsDirectory;
        this.SlnPath = slnPath;
        this.WebApiPath = webApiPath;
    }

    public string ArtifactsDirectory { get; }
    public string SlnPath { get; }
    public string WebApiPath { get; }
}
