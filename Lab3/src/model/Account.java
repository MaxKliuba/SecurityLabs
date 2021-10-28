package model;

import com.google.gson.annotations.SerializedName;

public class Account {
    @SerializedName("id")
    private int id;

    @SerializedName("money")
    private long money;

    @SerializedName("deletionTime")
    private String deletionTime;

    public Account(int id) {
        this.id = id;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public long getMoney() {
        return money;
    }

    public void setMoney(long money) {
        this.money = money;
    }

    public String getDeletionTime() {
        return deletionTime;
    }

    public void setDeletionTime(String deletionTime) {
        this.deletionTime = deletionTime;
    }

    @Override
    public String toString() {
        return String.format("{[id]: %s; [money]: %s; [deletionTime]: %s}", id, money, deletionTime);
    }
}
