use std::io::Read;
use std::fs::File;
use std::str::FromStr;

fn main() {
    let mut buffer = String::new();
    let mut file = File::open("input.txt").unwrap();
    file.read_to_string(&mut buffer).unwrap();

    let mut sum = 0u32;
    for line in buffer.lines() {
        sum += process_line_2(line);
    }
    println!("{}", sum);
}

fn process_line(line: &str) -> u32 {
    let numbers = line.split_whitespace()
        .map(|w| u32::from_str(w).unwrap())
        .collect::<Vec<u32>>();
    numbers.iter().max().unwrap() - numbers.iter().min().unwrap()
}

fn process_line_2(line: &str) -> u32 {
    let mut numbers = line.split_whitespace()
        .map(|w| u32::from_str(w).unwrap())
        .collect::<Vec<u32>>();

    numbers.sort_by(|a, b| b.cmp(a));

    for i in 0 .. numbers.len() {
        for j in (i + 1) .. numbers.len() {
            if numbers[i] % numbers[j] == 0 {
                return numbers[i] / numbers[j]
            }
        }
    }
    0
}