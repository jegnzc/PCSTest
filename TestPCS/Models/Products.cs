namespace TestPCS.Models;

public record ProductReadResponse(string Id, string Name, string Description);
public record ProductCreateRequest(string Name, string Description);
public record ProductCreateResponse(string Id, string Name, string Description);
public record ProductUpdateRequest(string Name, string Description);
public record ProductUpdateResponse(string Id, string Name, string Description);