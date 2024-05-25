namespace Logic.Abstractions.Transport.Convertor;

public interface IConverter<Source,  Destination>
{
    public Destination Convert(Source source);
}
