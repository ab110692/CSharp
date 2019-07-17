package br.com.avancertecnologia.util.google.ads.enums;

public class TokenExtension {

    public static String GetTokenTestADS(Token tokenTest) {
        switch (tokenTest) {
            default:
            case BANNER:
                return "ca-app-pub-3940256099942544/6300978111";
            case Interstitial:
                return "ca-app-pub-3940256099942544/1033173712";
            case Interstitial_Video:
                return "ca-app-pub-3940256099942544/8691691433";
            case Rewarded_Video:
                return "ca-app-pub-3940256099942544/5224354917";
            case Native_Advanced:
                return "ca-app-pub-3940256099942544/2247696110";
            case Native_Advanced_Video:
                return "ca-app-pub-3940256099942544/1044960115";
        }
    }

}
