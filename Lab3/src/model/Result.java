package model;

import com.google.gson.annotations.SerializedName;

public class Result {
    @SerializedName("message")
    private String message;

    @SerializedName("account")
    private Account account;

    @SerializedName("realNumber")
    private int realNumber;

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public Account getAccount() {
        return account;
    }

    public void setAccount(Account account) {
        this.account = account;
    }

    public int getRealNumber() {
        return realNumber;
    }

    public void setRealNumber(int realNumber) {
        this.realNumber = realNumber;
    }

    @Override
    public String toString() {
        return String.format("{[message]: %s; [account]: %s; [realNumber]: %s}", message, account, realNumber);
    }
}
