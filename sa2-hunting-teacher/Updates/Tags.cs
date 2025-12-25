

#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

using System.Text.Json.Serialization;
using System.Text.Json;

namespace sa2_hunting_teacher.Updates {
	public partial class Tag {
		[JsonPropertyName("commit")]
		public Commit Commit { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("node_id")]
		public string NodeId { get; set; }

		[JsonPropertyName("tarball_url")]
		public Uri TarballUrl { get; set; }

		[JsonPropertyName("zipball_url")]
		public Uri ZipballUrl { get; set; }
	}

	public partial class Commit {
		[JsonPropertyName("sha")]
		public string Sha { get; set; }

		[JsonPropertyName("url")]
		public Uri Url { get; set; }
	}

	public partial class Tag {
		public static Tag[] FromJson(string json) => JsonSerializer.Deserialize<Tag[]>(json, Converter.Settings);
	}
}

#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603