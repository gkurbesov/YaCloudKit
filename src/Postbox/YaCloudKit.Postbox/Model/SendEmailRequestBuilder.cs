using System;
using System.Collections.Generic;
using YaCloudKit.Postbox.Model.Requests;

namespace YaCloudKit.Postbox;

public class SendEmailRequestBuilder
{
    private string? _fromEmailAddress = null;
    private readonly List<string> _toAddresses = new();
    private readonly List<string> _ccAddresses = new();
    private readonly List<string> _bccAddresses = new();
    private string? _rawContent = null;
    private EmailDataPart? _subject = null;
    private EmailBody? _body = null;


    public SendEmailRequestBuilder SetFromEmailAddress(string fromEmailAddress)
    {
        _fromEmailAddress = fromEmailAddress ?? throw new ArgumentNullException(nameof(fromEmailAddress));
        return this;
    }

    public SendEmailRequestBuilder AddToAddress(string toAddress)
    {
        _toAddresses.Add(toAddress ?? throw new ArgumentNullException(nameof(toAddress)));
        return this;
    }
    
    public SendEmailRequestBuilder AddToAddresses(params string[] toAddresses)
    {
        foreach (var toAddress in toAddresses)
        {
            _toAddresses.Add(toAddress ?? throw new ArgumentNullException(nameof(toAddress)));
        }
        return this;
    }

    public SendEmailRequestBuilder AddCcAddress(string ccAddress)
    {
        _ccAddresses.Add(ccAddress ?? throw new ArgumentNullException(nameof(ccAddress)));
        return this;
    }
    
    public SendEmailRequestBuilder AddCcAddresses(params string[] ccAddresses)
    {
        foreach (var ccAddress in ccAddresses)
        {
            _ccAddresses.Add(ccAddress ?? throw new ArgumentNullException(nameof(ccAddress)));
        }
        return this;
    }

    public SendEmailRequestBuilder AddBccAddress(string bccAddress)
    {
        _bccAddresses.Add(bccAddress ?? throw new ArgumentNullException(nameof(bccAddress)));
        return this;
    }
    
    public SendEmailRequestBuilder AddBccAddresses(params string[] bccAddresses)
    {
        foreach (var bccAddress in bccAddresses)
        {
            _bccAddresses.Add(bccAddress ?? throw new ArgumentNullException(nameof(bccAddress)));
        }
        return this;
    }

    public SendEmailRequestBuilder WithRawContent(string rawContent)
    {
        if (_subject != null || _body != null)
            throw new InvalidOperationException("Raw content cannot be used with simple content");

        _rawContent = rawContent ?? throw new ArgumentNullException(nameof(rawContent));
        return this;
    }

    public SendEmailRequestBuilder SetSubject(string subject, string charset = CharsetType.UTF8)
    {
        if (_rawContent != null)
            throw new InvalidOperationException("Simple content cannot be used with raw content");

        _subject = new EmailDataPart(subject ?? throw new ArgumentNullException(nameof(subject)), charset);
        return this;
    }

    public SendEmailRequestBuilder SetTextBody(string textBody, string charset = CharsetType.UTF8)
    {
        if (_rawContent != null)
            throw new InvalidOperationException("Simple content cannot be used with raw content");

        var dataPart = new EmailDataPart(textBody, charset);

        _body = _body is null
            ? new EmailBody(Text: dataPart, Html: null)
            : _body with { Text = dataPart };

        return this;
    }

    public SendEmailRequestBuilder SetHtmlBody(string htmlBody, string charset = CharsetType.UTF8)
    {
        if (_rawContent != null)
            throw new InvalidOperationException("Simple content cannot be used with raw content");

        var dataPart = new EmailDataPart(htmlBody, charset);

        _body = _body is null
            ? new EmailBody(Text: null, Html: dataPart)
            : _body with { Html = dataPart };

        return this;
    }

    public SendEmailRequest Build()
    {
        if (_fromEmailAddress == null)
            throw new InvalidOperationException("From email address is required");

        if (_toAddresses.Count == 0)
            throw new InvalidOperationException("At least one recipient is required");

        if (_rawContent != null || (_subject == null && _body == null))
            throw new InvalidOperationException("Either raw content or simple content must be provided");

        return new SendEmailRequest(FromEmailAddress: _fromEmailAddress,
            Destination: CreateEmailDestination(),
            Content: CreateEmailContent());
    }

    private EmailDestination CreateEmailDestination()
    {
        return new EmailDestination(ToAddresses: _toAddresses.ToArray(),
            CcAddresses: _ccAddresses.ToArray(),
            BccAddresses: _bccAddresses.ToArray());
    }

    private EmailContent CreateEmailContent()
    {
        if (_rawContent != null)
            return new EmailContent(Raw: new RawEmailContent(_rawContent), Simple: null);

        if (_subject != null && _body != null)
        {
            return new EmailContent(Simple: new SimpleEmailContent(_subject, _body), Raw: null);
        }

        if (_subject != null && _body == null)
            throw new InvalidOperationException("Text or HTML body for simple content is required");

        if (_subject == null && _body != null)
            throw new InvalidOperationException("Subject for simple content is required");

        throw new InvalidOperationException("Either raw content or simple content must be provided");
    }
}