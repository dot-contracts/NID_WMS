using System;

namespace wms_android.Models
{
    public class UpdateInfo
    {
        public string Version { get; set; } = string.Empty;
        public string CommitSha { get; set; } = string.Empty;
        public DateTime CommitDate { get; set; }
        public string CommitMessage { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public bool IsUpdateAvailable { get; set; }
    }
    
    public class GitHubCommit
    {
        public string Sha { get; set; } = string.Empty;
        public GitHubCommitDetails Commit { get; set; } = new();
    }
    
    public class GitHubCommitDetails
    {
        public string Message { get; set; } = string.Empty;
        public GitHubCommitAuthor Author { get; set; } = new();
    }
    
    public class GitHubCommitAuthor
    {
        public DateTime Date { get; set; }
    }
    
    public class GitHubRelease
    {
        public string Tag_name { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Published_at { get; set; }
        public GitHubAsset[] Assets { get; set; } = Array.Empty<GitHubAsset>();
    }
    
    public class GitHubAsset
    {
        public string Name { get; set; } = string.Empty;
        public string Browser_download_url { get; set; } = string.Empty;
        public long Size { get; set; }
    }
}