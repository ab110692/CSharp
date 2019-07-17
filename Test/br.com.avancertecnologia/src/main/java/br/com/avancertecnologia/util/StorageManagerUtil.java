package br.com.avancertecnologia.util;

import android.app.Activity;
import android.content.Context;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.Serializable;

import br.com.avancertecnologia.model.drink.Personagem;

public class StorageManagerUtil {

    public static void WriterFile(Activity activity, String path, Serializable content) throws IOException {
        String aux = new Gson().toJson(content);
        WriteFile(activity, path, aux);
    }

    public static void WriterFile(Activity activity, String path, String content) throws IOException{
        WriteFile(activity, path, content);
    }

    private static void WriteFile(Activity activity, String path, String content) throws IOException{
        FileOutputStream fos = activity.openFileOutput(path, Context.MODE_PRIVATE);
        fos.write(content.getBytes());
        fos.close();
    }

    public static String ReaderFileString(Activity activity, String path) throws IOException{
        return ReadeFile(activity, path);
    }

    public static Serializable ReaderFile(Activity activity, String path) throws IOException{
        String aux = ReadeFile(activity, path);
        return new Gson().fromJson(aux, Personagem.class);
    }

    private static String ReadeFile(Activity activity, String path) throws IOException{
        FileInputStream fis = activity.openFileInput(path);
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(fis));
        StringBuilder stringBuilder = new StringBuilder();
        String line;
        while((line = bufferedReader.readLine()) != null){
            stringBuilder.append(line);
            stringBuilder.append("\n");
        }
        bufferedReader.close();
        fis.close();
        return stringBuilder.toString();
    }


}
