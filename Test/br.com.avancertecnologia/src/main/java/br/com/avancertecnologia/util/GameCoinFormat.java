package br.com.avancertecnologia.util;

import android.support.annotation.NonNull;

import java.text.AttributedCharacterIterator;
import java.text.DecimalFormat;
import java.text.FieldPosition;
import java.text.Format;
import java.text.ParseException;
import java.text.ParsePosition;
import java.util.Map;
import java.util.NavigableMap;
import java.util.TreeMap;

public class GameCoinFormat extends Format {

    private NavigableMap<Double, String> suffixes = new TreeMap<>();
    private DecimalFormat decimalFormat;

    public GameCoinFormat(String pattern) {
        super();
        this.decimalFormat = new DecimalFormat(pattern);
        this.suffixes = new TreeMap<>();
        this.initSuffixes();
    }

    @Override
    public StringBuffer format(Object obj, @NonNull StringBuffer toAppendTo, @NonNull FieldPosition pos) {
        if (!(obj instanceof Double)) {
            throw new IllegalArgumentException("Somente número double");
        }
        Double value = (Double) obj;
        if (value == Double.MIN_VALUE) {
            return new StringBuffer().append(decimalFormat.format(value));
        }
        if (value < 0) {
            return new StringBuffer().append(decimalFormat.format(value));
        }
        if (value < 1000.00) {
            return new StringBuffer().append(decimalFormat.format(value));
        }
        Map.Entry<Double, String> e = suffixes.floorEntry(value);
        Double divideBy = e.getKey();
        String suffix = e.getValue();
        Double truncated = value / (divideBy / 10);
        boolean hasDecimal = truncated < 100 && (truncated / 10d) != (truncated / 10);
        return hasDecimal ? new StringBuffer().append(decimalFormat.format((truncated / 10d)) + suffix) : new StringBuffer().append(decimalFormat.format((truncated / 10)) + suffix);
    }

    @Override
    public AttributedCharacterIterator formatToCharacterIterator(@NonNull Object obj) {
        return super.formatToCharacterIterator(obj);
    }

    @Override
    public Object parseObject(String source, @NonNull ParsePosition pos) {
        return null;
    }

    @Override
    public Object parseObject(String source) throws ParseException {
        return super.parseObject(source);
    }

    @Override
    public Object clone() {
        return super.clone();
    }

    private void initSuffixes() {
        suffixes.put(1000.00, "K");
        suffixes.put(1000000.00, "M");
        suffixes.put(1000000000.00, "B");
        suffixes.put(1000000000000.00, "T");
        suffixes.put(1000000000000000.00, "a");
        suffixes.put(1000000000000000000.00, "b");
        suffixes.put(1000000000000000000000.00, "c");
        suffixes.put(1000000000000000000000000.00, "d");
        suffixes.put(1000000000000000000000000000.00, "e");
        suffixes.put(1000000000000000000000000000000.00, "f");
        suffixes.put(1000000000000000000000000000000000.00, "g");
        suffixes.put(1000000000000000000000000000000000000.00, "h");
        suffixes.put(1000000000000000000000000000000000000000.00, "i");
        suffixes.put(1000000000000000000000000000000000000000000.00, "j");
        suffixes.put(1000000000000000000000000000000000000000000000.00, "k");
        suffixes.put(1000000000000000000000000000000000000000000000000.00, "l");
        suffixes.put(1000000000000000000000000000000000000000000000000000.00, "m");
    }


}
