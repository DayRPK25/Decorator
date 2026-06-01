public abstract class ScriptDecorator : IScript {
    protected readonly IScript _inner;

    protected ScriptDecorator(IScript inner) {
        _inner = inner;
    }

    public virtual string getPath() => _inner.getPath();
    public virtual string getText() => _inner.getText();
}