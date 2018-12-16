using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RemoteNotes.Specs
{
  [Binding]
  public class NotesSteps
  {
    private ApiContext _apiContext;
    private Note _returnedNote;
    private List<Note> _returnedNotes;
    private string _noteBody;

    public NotesSteps(ApiContext apiContext)
    {
      _apiContext = apiContext;
    }

    [Given(@"I have created a note with current time in body")]
    [When(@"I create note with current time in body")]
    public async Task GivenIHaveCreatedANoteWithCurrentTimeInBody()
    {
      _noteBody = DateTime.Now.ToLongTimeString();

      try
      {
        _returnedNote = await _apiContext.ApiClient.CreateNote(_noteBody);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [When(@"I request notes list")]
    public async Task WhenIRequestNotesList()
    {
      try
      {
        _returnedNotes = await _apiContext.ApiClient.GetNotes();
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [When(@"I update the created note with body ""(.*)""")]
    public async Task WhenIUpdateTheCreatedNoteWithBody(string body)
    {
      _noteBody = body;
      try
      {
        _returnedNote = await _apiContext.ApiClient.UpdateNote(_returnedNote.Id, _noteBody);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [When(@"I delete the created note")]
    public async Task WhenIDeleteTheCreatedNote()
    {
      try
      {
        await _apiContext.ApiClient.DeleteNote(_returnedNote.Id);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [Then(@"list of notes is returned")]
    public void ThenListOfNotesIsReturned()
    {
      Assert.IsTrue(_returnedNotes.Count > 0);
    }

    [Then(@"note with the same body is returned")]
    public void ThenNoteWithTheSameBodyIsReturned()
    {
      Assert.AreEqual(_noteBody, _returnedNote.Body);
    }
  }
}
