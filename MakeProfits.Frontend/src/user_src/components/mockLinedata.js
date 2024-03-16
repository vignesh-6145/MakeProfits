import { tokens } from '../theme'; // Assuming you have the theme tokens imported

export const mockLineData = [
  {
    id: 'stocks',
    color: tokens('dark').greenAccent[500],
    data: [
      { x: 'Jan', y: 100 },
      { x: 'Feb', y: 120 },
      { x: 'Mar', y: 110 },
      { x: 'Apr', y: 130 },
      { x: 'May', y: 125 },
      { x: 'Jun', y: 140 },
      { x: 'Jul', y: 145 },
      { x: 'Aug', y: 150 },
      { x: 'Sep', y: 155 },
      { x: 'Oct', y: 160 },
      { x: 'Nov', y: 165 },
      { x: 'Dec', y: 170 },
    ],
  },
  {
    id: 'bonds',
    color: tokens('dark').blueAccent[300],
    data: [
      { x: 'Jan', y: 50 },
      { x: 'Feb', y: 55 },
      { x: 'Mar', y: 60 },
      { x: 'Apr', y: 65 },
      { x: 'May', y: 70 },
      { x: 'Jun', y: 75 },
      { x: 'Jul', y: 80 },
      { x: 'Aug', y: 85 },
      { x: 'Sep', y: 90 },
      { x: 'Oct', y: 95 },
      { x: 'Nov', y: 100 },
      { x: 'Dec', y: 105 },
    ],
  },
  {
    id: 'mutualFunds',
    color: tokens('dark').redAccent[200],
    data: [
      { x: 'Jan', y: 80 },
      { x: 'Feb', y: 85 },
      { x: 'Mar', y: 90 },
      { x: 'Apr', y: 95 },
      { x: 'May', y: 100 },
      { x: 'Jun', y: 105 },
      { x: 'Jul', y: 110 },
      { x: 'Aug', y: 115 },
      { x: 'Sep', y: 120 },
      { x: 'Oct', y: 125 },
      { x: 'Nov', y: 130 },
      { x: 'Dec', y: 135 },
    ],
  },
];