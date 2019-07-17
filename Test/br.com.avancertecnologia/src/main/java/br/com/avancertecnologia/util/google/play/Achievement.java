package br.com.avancertecnologia.util.google.play;

import android.app.Activity;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.view.Gravity;
import android.widget.Toast;

import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.games.Games;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;

import br.com.avancertecnologia.gui.ToastManager;

public class Achievement {

    public static final int RC_ACHIEVEMENT_UI = 9003;


    public static void unlockingAchievementToast(final Activity activity, String achievementID, final String achievementName, final int image) {
        Games.getAchievementsClient(activity, GoogleSignIn.getLastSignedInAccount(activity)).unlockImmediate(achievementID).addOnCompleteListener(new OnCompleteListener<Void>() {
            @Override
            public void onComplete(@NonNull Task<Void> task) {
                if(task.isSuccessful()){
                    ToastManager.makeTextAchievement(activity, achievementName, Gravity.TOP, Toast.LENGTH_SHORT, image).show();
                }
            }
        });
    }

    public static void unlockingAchievement(final Activity activity, String achievementID) {
        Games.getAchievementsClient(activity, GoogleSignIn.getLastSignedInAccount(activity)).unlock(achievementID);
    }

    public static void incrementAchievement(Activity activity, String achievementID, int increment) {
        Games.getAchievementsClient(activity, GoogleSignIn.getLastSignedInAccount(activity))
                .increment(achievementID, increment);
    }

    public static void showAchievements(final Activity activity) {
        Games.getAchievementsClient(activity, GoogleSignIn.getLastSignedInAccount(activity))
                .getAchievementsIntent()
                .addOnSuccessListener(new OnSuccessListener<Intent>() {
                    @Override
                    public void onSuccess(Intent intent) {
                        activity.startActivityForResult(intent, RC_ACHIEVEMENT_UI);
                    }
                });

    }
}
