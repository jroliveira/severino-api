#addin "Cake.Docker&version=0.9.7"
#addin "Cake.Figlet&version=1.2.0"

#load "build/*.cake"

Setup<BuildData>(context =>
{
    Information(Figlet("Severino"));

    return new BuildData(
        context,
        GetConfiguration(),
        ErrorHandler,
        new RepositoryData(
            "origin"),
        new SolutionData(
            "./artifacts",
            "./Severino.sln",
            "./src/Severino.WebApi/"));
});

Task("Default")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution");

Task("Deploy")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Publish-WebApi");

RunTarget(Argument("target", "Default"));
