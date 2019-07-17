package br.com.avancertecnologia.util.model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class DateTimeUTC implements Serializable{
    @SerializedName("$id")
    public long id;
    public String currentDateTime;
    public String utcOffset;
    public boolean isDayLightSavingsTime;
    public String dayOfTheWeek;
    public String timeZoneName;
    public long currentFileTime;
    public String ordinalDate;
    public String serviceResponse;
}
