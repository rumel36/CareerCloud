package com.businessdoors.mvvmnoteapp;

import android.app.Application;
import android.os.AsyncTask;

import androidx.lifecycle.LiveData;

import java.util.List;

public class NoteRepository{
    private NoteDao noteDao;
    private LiveData<List<Note>> getAllNotes;
    public NoteRepository(Application application){
        NoteDatabase noteDatabase = NoteDatabase.getDbInstance(application);
        noteDao = noteDatabase.noteDao();
        getAllNotes = noteDao.getAllNotes();
    }

    public void insert(Note note){
        new InsertTask(noteDao).execute(note);
    }
    public void update(Note note){
        new UpdateTask(noteDao).execute(note);
    }
    public void delete(Note note){
        new DeleteTask(noteDao).execute(note);
    }
    public void deleteAll(){
        new DeleteAllTask(noteDao).execute();
    }
    public LiveData<List<Note>> getGetAllNotes() {
        return getAllNotes;
    }
     //We should make it static otherwise it will create memory leak. We should not create instance
    private static class InsertTask extends AsyncTask<Note, Void, Void>{
        private NoteDao noteDao;
        public InsertTask(NoteDao noteDao){
            this.noteDao = noteDao;
        }
        @Override
        protected Void doInBackground(Note... notes) {
            noteDao.insert(notes[0]);
            return null;
        }
    }

    private static class UpdateTask extends AsyncTask<Note, Void, Void>{
        private NoteDao noteDao;
        public UpdateTask(NoteDao noteDao){
            this.noteDao = noteDao;
        }
        @Override
        protected Void doInBackground(Note... notes) {
            noteDao.update(notes[0]);
            return null;
        }
    }
    private static class DeleteTask extends AsyncTask<Note, Void, Void>{
        private NoteDao noteDao;
        public DeleteTask(NoteDao noteDao){
            this.noteDao = noteDao;
        }
        @Override
        protected Void doInBackground(Note... notes) {
            noteDao.delete(notes[0]);
            return null;
        }
    }

    private static class DeleteAllTask extends AsyncTask<Void, Void, Void>{
        private NoteDao noteDao;
        public DeleteAllTask(NoteDao noteDao){
            this.noteDao = noteDao;
        }
        @Override
        protected Void doInBackground(Void... voids) {
            noteDao.deleteAllNotes();
            return null;
        }
    }

}
