package br.com.avancertecnologia.gui;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import br.com.avancertecnologia.R;

/**
 * TODO: document your custom view class.
 */
public class ToastManager extends Toast {


    /**
     * Construct an empty Toast object.  You must call {@link #setView} before you
     * can call {@link #show}.
     *
     * @param context The context to use.  Usually your {@link Application}
     *                or {@link Activity} object.
     */
    public ToastManager(Context context) {
        super(context);
    }

    public static Toast makeText(Context context, String message, int gravity, int duration, int image){
        View view = LayoutInflater.from(context).inflate(R.layout.toast_manager, null, false);
        LinearLayout linearLayout = (LinearLayout) view.findViewById(R.id.toast_type);
        linearLayout.setBackgroundResource(R.drawable.toast_default);

        TextView messageTextView = view.findViewById(R.id.toast_text);
        ImageView imageView = view.findViewById(R.id.toast_icon);

        messageTextView.setText(message);
        imageView.setImageResource(image);

        Toast toast = new Toast(context);
        toast.setDuration(duration);
        toast.setView(view);
        toast.setGravity(gravity, 0, 0);
        return toast;
    }

    public static Toast makeTextAchievement(Context context, String message, int gravity, int duration, int image){
        View view = LayoutInflater.from(context).inflate(R.layout.toast_manager, null, false);
        LinearLayout linearLayout = view.findViewById(R.id.toast_type);
        linearLayout.setBackgroundResource(R.drawable.toast_default);

        //Change visibily
        linearLayout = view.findViewById(R.id.toast_achievement_linear_layout);
        linearLayout.setVisibility(View.VISIBLE);

        linearLayout = view.findViewById(R.id.toast_text_linear_layout);
        linearLayout.setVisibility(View.GONE);

        ImageView achievementImageView = view.findViewById(R.id.toast_icon_echievement);
        achievementImageView.setVisibility(View.VISIBLE);
        //end

        TextView messageTextView = view.findViewById(R.id.toast_text);
        ImageView imageView = view.findViewById(R.id.toast_icon);

        messageTextView.setText(message);
        imageView.setImageResource(image);

        Toast toast = new Toast(context);
        toast.setDuration(duration);
        toast.setView(view);
        toast.setGravity(gravity, 0, 0);
        return toast;
    }

}
