namespace account_service.ValueObjects;

public struct NumPaintersAndUsers
{
    public int numPainters { get; set; }
    public int numUsers { get; set; }

    public NumPaintersAndUsers(int numPainters, int numUsers)
    {
        this.numPainters = numPainters;
        this.numUsers = numUsers;
    }
}