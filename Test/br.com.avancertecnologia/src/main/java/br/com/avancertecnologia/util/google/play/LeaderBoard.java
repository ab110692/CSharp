package br.com.avancertecnologia.util.google.play;

import android.app.Activity;
import android.content.Intent;

import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.games.Games;
import com.google.android.gms.tasks.OnSuccessListener;

public class LeaderBoard {

    public static final int RC_LEADERBOARD_UI = 9004;

    public static void updatePlayerScore(final Activity activity, final String leaderBoardID, final double value) {
        Games.getLeaderboardsClient(activity, GoogleSignIn.getLastSignedInAccount(activity)).submitScore(leaderBoardID, (long)value);
    }

    public static void showLeaderboard(final Activity activity, String leaderBoardID) {
        Games.getLeaderboardsClient(activity, GoogleSignIn.getLastSignedInAccount(activity))
                .getAllLeaderboardsIntent()
                .addOnSuccessListener(new OnSuccessListener<Intent>() {
                    @Override
                    public void onSuccess(Intent intent) {
                        activity.startActivityForResult(intent, RC_LEADERBOARD_UI);
                    }
                });
    }

}
