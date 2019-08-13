namespace SpeechRecognition.Data.Entities
{
    public class ResponseResult
    {
        public ResponseResult()
        {
        }
        public ResponseResult(string content,string collName)
        {
            ContentVideo = content;
            Coll_name = collName;
        }
        public string ContentVideo { get; set; }
        public string Coll_name { get; set; }
    }
}
