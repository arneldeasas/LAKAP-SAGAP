﻿namespace LAKAPSAGAP.Models.Models;

public class CommonModel
{
    [Key]
    public string Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string? AddedById { get; set; } //userid of the user who added the stock type
    public UserInfo? AddedBy { get; set; }

    public string? LastModifiedById { get; set; }
    public UserInfo? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
    public bool isArchived { get; set; }
}
