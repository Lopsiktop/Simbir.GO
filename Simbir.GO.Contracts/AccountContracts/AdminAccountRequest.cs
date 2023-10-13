namespace Simbir.GO.Contracts.AccountContracts;

public record AdminAccountRequest(string Username, string Password, bool IsAdmin, double Balance);