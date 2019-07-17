package br.com.avancertecnologia.util.google.ads;

import android.app.Activity;

import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.MobileAds;
import com.google.android.gms.ads.reward.RewardedVideoAd;
import com.google.android.gms.ads.reward.RewardedVideoAdListener;

public class RewardVideo {

    private RewardedVideoAd rewardedVideoAd;
    private Activity activity;
    private String token;

    public RewardVideo(Activity activity, String id_app) {
        if (activity == null) {
            throw new NullPointerException("context não pode ser nulo!");
        }
        if (id_app.isEmpty()) {
            throw new NullPointerException("Token não pode ser nulo!");
        }

        this.activity = activity;
        this.token = id_app;
        MobileAds.initialize(this.activity, this.token);
        rewardedVideoAd = MobileAds.getRewardedVideoAdInstance(this.activity);
    }

    public void load(String ads) {
        if(!rewardedVideoAd.isLoaded()) {
            rewardedVideoAd.loadAd(ads, new AdRequest
                    .Builder()
                    .addTestDevice("C4AED2DBB4A8FADCDEBADD6661FC6A6C")
                    .addTestDevice("B623998E8D9FDE2ABEE6F0588B0A7C32")
                    .build());
        }
    }

    public void setListernerEvent(RewardedVideoAdListener rewardVideoListener) {
        this.rewardedVideoAd.setRewardedVideoAdListener(rewardVideoListener);
    }

    public void show() {
        if (this.rewardedVideoAd.isLoaded()) {
            this.rewardedVideoAd.show();
        }
    }

    public void onResumo(Activity activity){
        rewardedVideoAd.resume(activity);
    }

    public void onPause(Activity activity){
        rewardedVideoAd.pause(activity);
    }

    public void onDestroy(Activity activity){
        rewardedVideoAd.destroy(activity);
    }


}
