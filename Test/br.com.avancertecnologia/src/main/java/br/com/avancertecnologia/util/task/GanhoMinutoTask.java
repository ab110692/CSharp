package br.com.avancertecnologia.util.task;

import android.app.Activity;

import java.util.Timer;
import java.util.TimerTask;

import br.com.avancertecnologia.util.task.listeners.TaskListener;

public class GanhoMinutoTask extends TimerTask {

    private Object obj = new Object();

    private Activity act;
    private Timer timer;
    private TaskListener taskListener;

    private double valorDinamico;
    private double valorStatico;
    private double valorSegundo;
    private boolean hasValueDinamico;
    private boolean hasValueStatico;

    private double totalDimanico;
    private double totalStatico;

    public GanhoMinutoTask(Activity act) {
        super();
        this.act = act;
        this.timer = new Timer();
        this.timer.scheduleAtFixedRate(this, 1000, 1000);
        this.cliqueSegundoTask();
    }

    @Override
    public void run() {
        act.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                synchronized (obj) {
                    if (hasValueDinamico) {
                        totalDimanico = (valorSegundo * 60) + valorStatico;
                        hasValueDinamico = false;
                    }
                    if (hasValueStatico) {
                        totalStatico += valorStatico;
                        hasValueStatico = false;
                    }
                    totalDimanico -= valorSegundo;
                    if (totalDimanico <= 0) {
                        totalDimanico = 0;
                    }
                    if (taskListener != null) {
                        taskListener.atualizar(totalDimanico + totalStatico);
                    }
                }
            }
        });
    }

    @Override
    public boolean cancel() {
        return super.cancel();
    }

    @Override
    public long scheduledExecutionTime() {
        return super.scheduledExecutionTime();
    }

    public void adicionarGanhoDimanico(double valor) {
        this.valorDinamico += valor;
        this.hasValueDinamico = true;
    }

    public void adicionarGanhoStatico(double valor, long time) {
        this.valorStatico += (60000 / time) * valor;
        this.hasValueStatico = true;
    }

    public void removerGanhoStatico(double valor, long time) {
        this.valorStatico -= (60000 / time) * valor;
    }

    public void setTaskListener(TaskListener taskListener) {
        this.taskListener = taskListener;
    }

    private void cliqueSegundoTask() {
        Timer removerTimer = new Timer();
        removerTimer.scheduleAtFixedRate(new TimerTask() {
            @Override
            public void run() {
                synchronized (obj) {
                    if (valorDinamico > 0) {
                        valorSegundo = valorDinamico;
                    }
                    valorDinamico = 0;
                }
            }
        }, 1000, 1000);
    }
}
