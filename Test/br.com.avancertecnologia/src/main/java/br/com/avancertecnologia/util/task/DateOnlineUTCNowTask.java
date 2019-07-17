package br.com.avancertecnologia.util.task;

import android.os.AsyncTask;

import java.util.Date;

import br.com.avancertecnologia.util.DateUtil;
import br.com.avancertecnologia.util.task.listeners.AsyncTaskListener;

public class DateOnlineUTCNowTask extends AsyncTask<String, Void, Date> {

    public AsyncTaskListener delegate = null;

    public DateOnlineUTCNowTask() {
    }

    public void setAsyncTaskListener(AsyncTaskListener asyncTaskListener) {
        this.delegate = asyncTaskListener;
    }

    @Override
    protected Date doInBackground(String... string) {
        try {
            return DateUtil.FromNTPServer();
        } catch (Exception ex) {
            ex.printStackTrace();
            return null;
        }
    }

    @Override
    protected void onPostExecute(Date date) {
        delegate.onPostExecute(date);
        super.onPostExecute(date);
    }
}
