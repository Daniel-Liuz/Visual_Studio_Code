using System.Collections.Generic;
using System.Text.Json.Serialization; // 确保有这一行，为了使用 [JsonPropertyName] 特性

namespace WindowsFormsApp1 // 确保这里的命名空间和你的项目一致
{
    /// <summary>
    /// 定义单个关系的数据结构
    /// </summary>
    internal class ExtractedRelation
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("relation")]
        public string Relation { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }
    }

    /// <summary>
    /// 定义从Python后端返回的完整JSON响应的数据结构
    /// </summary>
    internal class RelationExtractionResponse
    {
        [JsonPropertyName("original_text")]
        public string OriginalText { get; set; }

        // !!! 这就是你一直缺少的字段，现在已经加上了 !!!
        [JsonPropertyName("extracted_people")]
        public List<string> ExtractedPeople { get; set; }

        [JsonPropertyName("extracted_relations")]
        public List<ExtractedRelation> ExtractedRelations { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }

    /// <summary>
    /// 定义发送到Python后端的请求体的数据结构
    /// </summary>
    internal class RelationExtractionRequest
    {
        public string Text { get; set; }
    }
}
