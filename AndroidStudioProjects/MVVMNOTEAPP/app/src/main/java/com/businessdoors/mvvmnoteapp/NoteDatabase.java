package com.businessdoors.mvvmnoteapp;

import android.content.Context;
import android.os.AsyncTask;

import androidx.annotation.NonNull;
import androidx.room.Database;
import androidx.room.Room;
import androidx.room.RoomDatabase;
import androidx.sqlite.db.SupportSQLiteDatabase;

@Database(entities = {Note.class}, version = 1)
public abstract class NoteDatabase extends RoomDatabase {
  private static NoteDatabase dbInstance;
  public abstract NoteDao noteDao();

  public static synchronized NoteDatabase getDbInstance(Context context){
      if(dbInstance==null){
          dbInstance = Room.databaseBuilder(context.getApplicationContext(),NoteDatabase.class, "note_database")
                       .fallbackToDestructiveMigration()//If version mismatched then app will be crashed.
                       .addCallback(dbCallback)                                // fallbackToDescrtuctiveMigration Saves from crash.
                       .build();
      }
      return dbInstance;
  }

  private static RoomDatabase.Callback dbCallback = new RoomDatabase.Callback(){
      @Override
      public void onCreate(@NonNull SupportSQLiteDatabase db) {
          super.onCreate(db);
          new PreLoadNotes(dbInstance).execute();
      }
  };
  private static class PreLoadNotes extends AsyncTask<Void, Void, Void>{
      private NoteDao noteDao;
      private  PreLoadNotes(NoteDatabase db){
         noteDao = db.noteDao();
     }
      @Override
      protected Void doInBackground(Void... voids) {
         noteDao.insert(new Note("Title1", "Description1", 1));
         noteDao.insert(new Note("Title2", "Description2", 2));
         noteDao.insert(new Note("Title3", "Description3", 3));
         return null;
      }
  }
}

