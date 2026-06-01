public class ConcreteScript : IScript {
    private readonly string _path;
    private readonly string _text;

    public ConcreteScript(string path, string text) {
        _path = path;
        _text = text;
    }

    public string getPath() => _path;
    public string getText() => _text;
}