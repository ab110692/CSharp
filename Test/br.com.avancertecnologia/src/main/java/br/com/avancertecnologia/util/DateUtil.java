package br.com.avancertecnologia.util;

import com.google.gson.Gson;

import java.io.IOException;
import java.net.URL;
import java.util.Calendar;
import java.util.Date;
import java.util.TimeZone;

import br.com.avancertecnologia.util.model.DateTimeUTC;

public class DateUtil {

    public static final String WORLD_CLOCK_API = "http://worldclockapi.com/api/json/utc/now";

    public static Date FromDateOnlineUTCNow(String url) throws IOException {
        Connection connection = new Connection(new URL(url));
        connection.setTimeOutConnection(30000);
        connection.setTimeOutReader(30000);
        connection.connect();
        DateTimeUTC dateTimeUTC = new Gson().fromJson(connection.getContent(), DateTimeUTC.class);
        Date dateTime = new Date();
        dateTime.setTime(dateTimeUTC.currentFileTime);
        connection.closeConnection();
        return dateTime;
    }

    public static Date FromNTPServer(){
        long nowAsPerDeviceTimeZone = 0;
        SntpClient sntpClient = new SntpClient();

        if (sntpClient.requestTime("pool.ntp.br", 30000)) {
            nowAsPerDeviceTimeZone = sntpClient.getNtpTime();
            Calendar cal = Calendar.getInstance();
            TimeZone timeZoneInDevice = cal.getTimeZone();
            int differentialOfTimeZones = timeZoneInDevice.getOffset(System.currentTimeMillis());
            nowAsPerDeviceTimeZone -= differentialOfTimeZones;
        }
        return new Date(nowAsPerDeviceTimeZone);
    }

    public static Date FromDate(long tempo) {
        Date d = new Date(1900, 1, 1, FromHour(tempo), FromMinute(tempo), FromSegunds(tempo));
        return d;
    }

    public static int FromHour(long tempo) {
        int hour = 0;
        while ((((1000 * 60) * 60) <= tempo)) {
            tempo -= ((1000 * 60) * 60);
            hour++;
        }
        return hour;
    }

    public static int FromMinute(long tempo) {
        int minute = 0;
        while (((1000 * 60) <= tempo)) {
            tempo -= (1000 * 60);
            minute++;
        }
        return minute;
    }

    public static int FromSegunds(long tempo) {
        int segunds = 0;
        while (1000 <= tempo) {
            tempo -= 1000;
            segunds++;
        }
        return segunds;
    }

    private static long FromDifference(Date dateNow, Date dateBefore){
        long now = dateNow.getTime();
        long before = dateBefore.getTime();
        return now - before;
    }

    public static String format(long tempo){
        long time = tempo;
        int hora = DateUtil.FromHour(time);
        time -= hora * 1000 * 60 * 60;
        int minuto = DateUtil.FromMinute(time);
        time -= minuto * 1000 * 60;
        int segundo = DateUtil.FromSegunds(time);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(hora < 10 ? "0" + hora : hora);
        stringBuilder.append(":");
        stringBuilder.append(minuto < 10 ? "0" + minuto : minuto);
        stringBuilder.append(":");
        stringBuilder.append(segundo < 10 ? "0" + segundo : segundo);
        return stringBuilder.toString();
    }
}
