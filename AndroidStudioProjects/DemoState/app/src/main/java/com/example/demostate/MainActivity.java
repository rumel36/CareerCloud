package com.example.demostate;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;

public class MainActivity extends AppCompatActivity {


    ImageView image2 ;
    ImageView image ;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        image = findViewById(R.id.imageView);
        image2 = findViewById(R.id.imageView2);
        image.animate().alpha(0f).setDuration(2000);
    }

    public void changeImage(View v){

        Log.d("img", "image" +image);

        image.animate().alpha(0f).setDuration(2000);
        image2.animate().alpha(1f).setDuration(2000);

    }
    public void changeImage2(View v){

        image.animate().alpha(1f).setDuration(2000);
        image2.animate().alpha(0f).setDuration(2000);

    }

}