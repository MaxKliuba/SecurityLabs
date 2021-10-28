package task1;

import model.Account;
import model.Result;
import utils.CasinoRoyale;

import java.math.BigInteger;
import java.util.Arrays;

public class Task1 {

    public static void run(Account account) {
        playLcgWithCheat(account);
    }

    public static void playLcgWithCheat(Account account) {
        long[] states = new long[3];
        for (int i = 0; i < states.length; i++) {
            Result result = CasinoRoyale.playLcg(account.getId(), 1, 1);
            states[i] = result.getRealNumber();

            System.out.println(result);
        }
        System.out.println(Arrays.toString(states));

        long m = (long) Math.pow(2, 32);
        int a = crackUnknownMultiplier(states, m);
        int c = crackUnknownIncrement(states, m, a);
        System.out.println("a = " + a);
        System.out.println("c = " + c);

        long bet = 1;
        int next = 0;

        while (account.getMoney() < 1_000_000) {
            Result result = CasinoRoyale.playLcg(account.getId(), bet, next);

            if (result == null) {
                break;
            }

            account = result.getAccount();
            next = next(result.getRealNumber(), a, c, m);
            bet = account.getMoney() / 2;

            System.out.println(result);
        }
    }

    public static int next(int last, int a, int c, long m) {
        return (int) ((a * last + c) % m);
    }

    private static int crackUnknownIncrement(long[] states, long modulus, int multiplier) {
        return (int) ((states[1] - states[0] * multiplier) % modulus);
    }

    private static int crackUnknownMultiplier(long[] states, long modulus) {
        BigInteger modInv = BigInteger.valueOf(states[1] - states[0]).modInverse(BigInteger.valueOf(modulus));

        return (int) ((states[2] - states[1]) * modInv.intValue() % modulus);
    }
}
