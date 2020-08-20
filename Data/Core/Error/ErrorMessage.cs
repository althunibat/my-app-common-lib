// Created On: 2018.12.01
// Created By: Hamza Althunibat
// ----------------------------------------------------------------------------


namespace Godwit.Common.Data.Core.Error {
    public class ErrorMessage {
        public ErrorMessage(string language, string text) {
            Language = language;
            Text = text;
        }

        public string Language { get; }
        public string Text { get; }
    }
}