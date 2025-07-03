// 文件: WeiboPost.cs
using CsvHelper.Configuration.Attributes;

public class WeiboPost
{
    [Name("note_id")]
    public string NoteId { get; set; }
    [Name("content")]
    public string Content { get; set; }
    [Name("create_time")]
    public string CreateTime { get; set; }
    [Name("create_date_time")]
    public string CreateDateTime { get; set; }
    [Name("liked_count")]
    public int LikedCount { get; set; }
    [Name("comments_count")]
    public int CommentsCount { get; set; }
    [Name("shared_count")]
    public int SharedCount { get; set; }
    [Name("last_modify_ts")]
    public long LastModifyTs { get; set; }
    [Name("note_url")]
    public string NoteUrl { get; set; }
    [Name("ip_location")]
    public string IpLocation { get; set; }
    [Name("user_id")]
    public string UserId { get; set; }
    [Name("nickname")]
    public string Nickname { get; set; }
    [Name("gender")]
    public string Gender { get; set; }
    [Name("profile_url")]
    public string ProfileUrl { get; set; }
    [Name("avatar")]
    public string Avatar { get; set; }
    [Name("source_keyword")]
    public string SourceKeyword { get; set; }
}
