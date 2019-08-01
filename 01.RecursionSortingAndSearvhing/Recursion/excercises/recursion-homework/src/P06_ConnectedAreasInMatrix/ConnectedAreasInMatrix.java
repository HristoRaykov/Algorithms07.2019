package P06_ConnectedAreasInMatrix;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.TreeSet;

public class ConnectedAreasInMatrix {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int rowsNum = Integer.parseInt(reader.readLine());
        int colsNum = Integer.parseInt(reader.readLine());
        char[][] arr = new char[rowsNum][colsNum];
        for (int row = 0; row < rowsNum; row++) {
            String inputRow = reader.readLine();
            for (int col = 0; col < colsNum; col++) {
                arr[row][col] = inputRow.charAt(col);
            }
        }
        TreeSet<ConnectedArea> connAreas = findConnectedAreas(arr);
        int ctr = 1;
        System.out.println("Total areas found: " + connAreas.size());
        for (ConnectedArea connArr : connAreas) {
            System.out.println(String.format("Area #%s at %s", ctr, connArr.toString()));
            ctr++;
        }

    }

    private static TreeSet<ConnectedArea> findConnectedAreas(char[][] arr) {
        TreeSet<ConnectedArea> connAreas = new TreeSet<>();

        while (true) {
            Cell position = findTraversableCell(arr);
            if (position == null){
                return connAreas;
            }

            ConnectedArea connArr = new ConnectedArea(position);
            findConnectedAreaByCell(arr, connArr, position);
            connAreas.add(connArr);
        }
    }

    private static void findConnectedAreaByCell(char[][] arr, ConnectedArea connArr, Cell cell) {
        if (!isValidCell(arr, cell)){
            return;
        }

        arr[cell.getRowIdx()][cell.getColIdx()] = 'x';
        connArr.addCellToArea(cell);

        findConnectedAreaByCell(arr, connArr, new Cell(cell.getRowIdx(), cell.getColIdx()-1));
        findConnectedAreaByCell(arr, connArr, new Cell(cell.getRowIdx(), cell.getColIdx()+1));
        findConnectedAreaByCell(arr, connArr, new Cell(cell.getRowIdx()-1, cell.getColIdx()));
        findConnectedAreaByCell(arr, connArr, new Cell(cell.getRowIdx()+1, cell.getColIdx()));
    }

    private static boolean isValidCell(char[][] arr, Cell cell) {
        int rowLen = arr.length;
        int colLen = arr[0].length;
        int rowIdx = cell.getRowIdx();
        int colIdx = cell.getColIdx();

        if (!(rowIdx >= 0 & rowIdx < rowLen & colIdx >= 0 & colIdx < colLen)) {
            return false;
        }

        return  arr[rowIdx][colIdx]==('-');
    }

    private static Cell findTraversableCell(char[][] arr) {
        for (int row = 0; row < arr.length; row++) {
            for (int col = 0; col < arr[0].length; col++) {
                if (arr[row][col]=='-') {
                    return new Cell(row, col);
                }
            }
        }

        return null;
    }
}


class Cell implements Comparable<Cell> {
    private int rowIdx;
    private int colIdx;

    public Cell() {
    }

    public Cell(int rowIdx, int colIdx) {
        this.rowIdx = rowIdx;
        this.colIdx = colIdx;
    }

    public int getRowIdx() {
        return rowIdx;
    }

    public int getColIdx() {
        return colIdx;
    }

    @Override
    public int compareTo(Cell cell) {
        if (this.rowIdx != cell.rowIdx) {
            return this.rowIdx - cell.rowIdx;
        } else {
            return this.colIdx - cell.colIdx;
        }
    }
}

class ConnectedArea implements Comparable<ConnectedArea> {
    private Cell position;
    private List<Cell> area;

    public ConnectedArea(Cell position) {
        this.position = position;
        this.area = new ArrayList<>();
    }

    public Cell getPosition() {
        return position;
    }

    public List<Cell> getArea() {
        return area;
    }

    public void addCellToArea(Cell cell) {
        this.area.add(cell);
    }

    @Override
    public int compareTo(ConnectedArea connectedArea) {
        if (this.area.size() != connectedArea.area.size()) {
            return connectedArea.area.size() - this.area.size();
        } else {
            return this.position.compareTo(connectedArea.position);
        }
    }

    @Override
    public String toString() {
        return String.format("(%s, %s), size: %s", position.getRowIdx(), position.getColIdx(), area.size());
    }
}
