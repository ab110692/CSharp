package br.com.avancertecnologia.animation;

import android.view.View;
import android.view.animation.AlphaAnimation;
import android.view.animation.AnimationSet;
import android.view.animation.LinearInterpolator;
import android.view.animation.RotateAnimation;
import android.view.animation.TranslateAnimation;

public class Animation {

    private static AnimationSet animationSet;

    public static android.view.animation.Animation TranslateAnimation(int x, int toX, int y, int toY, long duration, android.view.animation.Animation.AnimationListener animationListener){
        TranslateAnimation translateAnimation = new TranslateAnimation(x,toX,y,toY);
        translateAnimation.setDuration(duration);
        translateAnimation.setAnimationListener(animationListener);
        translateAnimation.setFillAfter(true);
        return translateAnimation;
    }

    public static android.view.animation.Animation AlphaAnimation(float fromAlpha, float toAlpha, long duration, android.view.animation.Animation.AnimationListener animationListener){
        AlphaAnimation alphaAnimation = new AlphaAnimation(fromAlpha, toAlpha);
        alphaAnimation.setDuration(duration);
        alphaAnimation.setFillAfter(true);
        alphaAnimation.setAnimationListener(animationListener);
        return alphaAnimation;
    }

    public static AnimationSet AddAnimation(android.view.animation.Animation animation){
        if(animationSet == null){
            animationSet = new AnimationSet(false);
            animationSet.setAnimationListener(new android.view.animation.Animation.AnimationListener() {
                @Override
                public void onAnimationStart(android.view.animation.Animation animation) {

                }

                @Override
                public void onAnimationEnd(android.view.animation.Animation animation) {
                    animationSet.cancel();
                    animationSet = null;
                }

                @Override
                public void onAnimationRepeat(android.view.animation.Animation animation) {

                }
            });
        }

        animationSet.addAnimation(animation);
        return animationSet;
    }

    public static android.view.animation.Animation StartInButton(View view){
        RotateAnimation rotate = new RotateAnimation(
                0, 360,
                android.view.animation.Animation.RELATIVE_TO_SELF, 0.4f,
                android.view.animation.Animation.RELATIVE_TO_SELF, 0.4f
        );
        rotate.setDuration(3000);
        rotate.setInterpolator(new LinearInterpolator());
        rotate.setRepeatCount(android.view.animation.Animation.INFINITE);
        rotate.setRepeatMode(android.view.animation.Animation.RESTART);
        return rotate;
    }
}
