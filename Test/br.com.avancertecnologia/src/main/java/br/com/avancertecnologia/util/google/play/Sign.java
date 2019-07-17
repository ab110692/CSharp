package br.com.avancertecnologia.util.google.play;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.widget.Toast;

import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInClient;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.Games;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;

public class Sign implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

    public static final int RC_SIGN_IN = 9001;

    public static boolean isSignedIn(Activity activity) {
        return GoogleSignIn.getLastSignedInAccount(activity) != null;
    }

    public void teste(final Activity activity){
        GoogleSignIn.getClient(activity,
                new GoogleSignInOptions
                        .Builder(GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN)
                        .requestEmail()
                        .build()).silentSignIn().addOnCompleteListener(new OnCompleteListener<GoogleSignInAccount>() {
            @Override
            public void onComplete(@NonNull Task<GoogleSignInAccount> task) {
                GoogleApiClient googleApiClient = new GoogleApiClient.Builder(activity)
                        .addConnectionCallbacks(Sign.this)
                        .addOnConnectionFailedListener(Sign.this)
                        .addApi(Games.API)
                        .setAccountName(task.getResult().getEmail())
                        .build();
                googleApiClient.connect();
                if (googleApiClient.isConnected()) {
                    Toast.makeText(activity, "Connected", Toast.LENGTH_LONG).show();
                } else {

                    Toast.makeText(activity, "fail", Toast.LENGTH_LONG).show();
                }
            }
        });
    }

    public static void signInSilently(Activity activity) {
        GoogleSignInClient signInClient = GoogleSignIn.getClient(activity,
                GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN);
        signInClient.silentSignIn().addOnCompleteListener(activity,
                new OnCompleteListener<GoogleSignInAccount>() {
                    @Override
                    public void onComplete(@NonNull Task<GoogleSignInAccount> task) {
                        if (task.isSuccessful()) {
                            // The signed in account is stored in the task's result.
                            GoogleSignInAccount signedInAccount = task.getResult();
                        } else {
                            // Player will need to sign-in explicitly using via UI
                        }
                    }
                });
    }

    public static void signInSilentlyWithRequestMail(Activity activity, @NonNull OnCompleteListener<GoogleSignInAccount> listener) {
        GoogleSignInClient signInClient = GoogleSignIn.getClient(activity,
                new GoogleSignInOptions
                        .Builder(GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN)
                        .requestEmail()
                        .build());
        signInClient.silentSignIn().addOnCompleteListener(activity,listener);
    }

    public static void startSignInIntent(Activity activity) {
        GoogleSignInClient signInClient = GoogleSignIn.getClient(activity,
                GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN);
        Intent intent = signInClient.getSignInIntent();
        activity.startActivityForResult(intent, RC_SIGN_IN);
    }

    public static void startSignInIntentWithRequestMail(Activity activity) {
        GoogleSignInClient googleSignInClient = GoogleSignIn.getClient(activity,
                new GoogleSignInOptions
                        .Builder(GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN)
                        .requestEmail()
                        .build());
        activity.startActivityForResult(googleSignInClient.getSignInIntent(), RC_SIGN_IN);
    }

    public static void signOut(Activity activity) {
        GoogleSignInClient signInClient = GoogleSignIn.getClient(activity,
                GoogleSignInOptions.DEFAULT_GAMES_SIGN_IN);
        signInClient.signOut().addOnCompleteListener(activity,
                new OnCompleteListener<Void>() {
                    @Override
                    public void onComplete(@NonNull Task<Void> task) {
                        // at this point, the user is signed out.
                    }
                });
    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {

    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }
}
