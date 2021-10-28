package utils;

import com.google.gson.Gson;
import model.Account;
import model.Result;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class CasinoRoyale {

    private static final String URI = "http://95.217.177.249/casino/";

    public static Account createAcc() {
        Account account = null;

        Gson gson = new Gson();
        int id = 0;

        while (account == null) {
            try {
                String jsonString = getUrlString(URI + "createacc?id=" + id);
                account = gson.fromJson(jsonString, Account.class);
            } catch (IOException e) {
                id++;
            }
        }

        return account;
    }

    public static Result playLcg(int id, long bed, int number) {
        return play("Lcg", id, bed, number);
    }

    public static Result playMt(int id, long bed, int number) {
        return play("Mt", id, bed, number);
    }

    public static Result playBetterMt(int id, long bed, int number) {
        return play("BetterMt", id, bed, number);
    }

    private static Result play(String mode, int id, long bed, int number) {
        Result result = null;

        Gson gson = new Gson();

        try {
            String jsonString = getUrlString(String.format("%splay%s?id=%s&bet=%s&number=%s", URI, mode, id, bed, number));
            result = gson.fromJson(jsonString, Result.class);
        } catch (IOException e) {
        }

        return result;
    }

    public static String getUrlString(String urlSpec) throws IOException {
        URL url = new URL(urlSpec);

        HttpURLConnection connection = (HttpURLConnection) url.openConnection();

        try {
            BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));

            String inputLine;
            StringBuilder response = new StringBuilder();
            while ((inputLine = in.readLine()) != null) {
                response.append(inputLine);
            }
            in.close();

            return response.toString();

        } catch (IOException e) {
            StringBuilder error = new StringBuilder();
            InputStream errorStream = connection.getErrorStream();

            if (errorStream != null) {
                BufferedReader in = new BufferedReader(new InputStreamReader(errorStream));

                String errorLine;

                while ((errorLine = in.readLine()) != null) {
                    error.append(errorLine);
                }
                in.close();

            } else {
                error.append("null");
            }

            System.out.printf("[%s]: %s - %s - %s%n",
                    connection.getResponseCode(), connection.getResponseMessage(), error, urlSpec);

            throw e;
        }
    }
}
