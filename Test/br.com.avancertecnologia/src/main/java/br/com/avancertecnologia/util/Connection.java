package br.com.avancertecnologia.util;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Connection {

    private HttpURLConnection httpURLConnection;
    private BufferedReader bufferedReader;

    public Connection(URL url) throws IOException{
        this.httpURLConnection = (HttpURLConnection) url.openConnection();
    }

    public int connect() throws IOException{
        this.httpURLConnection.connect();
        return this.httpURLConnection.getResponseCode();
    }

    public void setTimeOutConnection(int time){
        this.httpURLConnection.setConnectTimeout(time);
    }

    public void setTimeOutReader(int time){
        this.httpURLConnection.setReadTimeout(time);
    }

    public String getContent() throws IOException{
        InputStream inputStream = this.httpURLConnection.getInputStream();
        this.bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
        StringBuffer buffer = new StringBuffer();
        String line;
        while((line = this.bufferedReader.readLine()) != null){
            buffer.append(line);
            buffer.append("\n");
        }
        inputStream.close();
        bufferedReader.close();
        return buffer.toString();
    }

    public void closeConnection(){
        this.httpURLConnection.disconnect();
    }
}
